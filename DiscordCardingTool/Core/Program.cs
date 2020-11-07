using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Utils;
using DiscordCardingTool.JSON;
using DiscordCardingTool.Utils;

namespace DiscordCardingTool
{
    class Program
    {
        static List<string> Tokens = new List<string>();

        static void Main(string[] args)
        {
            ConsoleUtils.SetTitle("Discord Carding Tool - Yaekith and tostring");

            try
            {
                if (!File.Exists("Tokens.txt"))
                {
                    File.Create("Tokens.txt").Close();
                    ConsoleUtils.LogError("No tokens were found. Please insert some into Tokens.txt and try again.");
                    Console.ReadLine();
                }
                else
                {
                    Tokens = File.ReadAllLines("Tokens.txt").ToList();
                    ConsoleUtils.SetTitle($"Discord Carding Tool - Yaekith and tostring - {Tokens.Count} Token(s)");
                    ConsoleUtils.Log("Welcome to the most elite tool on the fucking platform");
                    ConsoleUtils.Log("Enter Option: (1 - Nitro Classic, 2 - Nitro, 3 - Boosts)");

                    int option = int.Parse(Console.ReadLine());
                    switch (option)
                    {
                        case 1:
                            ConsoleUtils.Log("Enter Duration Option: (1 - Month, 2 - Year)");
                            int duration = int.Parse(Console.ReadLine());

                            if (duration == 1)
                            {
                                ConsoleUtils.Log("Enter Amount: ");
                                int amount = int.Parse(Console.ReadLine());

                                foreach(var token in Tokens)
                                {
                                    HttpClient client = new HttpClient();
                                    client.DefaultRequestHeaders.Add("Authorization", token);
                                    var response = client.GetAsync("https://discord.com/api/v8/users/@me/billing/payment-sources").Result;
                                    
                                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                    {
                                        var paymentsources = JsonConvert.DeserializeObject<List<PaymentSource>>(response.Content.ReadAsStringAsync().Result);
                                        
                                        if (paymentsources.Count > 0)
                                        {
                                            ConsoleUtils.Log($"Token: {token} has {paymentsources.Count} payment source(s). Trying to purchase {amount} classic monthly gift code(s).");

                                            for(int j = 0; j < paymentsources.Count; j++)
                                            {
                                                for (int i = 0; i < amount; i++)
                                                {
                                                    var purchaseBody = new PurchaseNitroBody()
                                                    {
                                                        gift = true,
                                                        sku_subscription_plan_id = "511651871736201216",
                                                        expected_amount = 499,
                                                        payment_source_id = paymentsources[j].id
                                                    };

                                                    var purchaseResponse = client.PostAsync("https://discord.com/api/v8/store/skus/521846918637420545/purchase", new StringContent(JsonConvert.SerializeObject(purchaseBody), Encoding.UTF8, "application/json")).Result;
                                                    var purchaseResponseBody = JsonConvert.DeserializeObject<PurchaseNitroResponse>(purchaseResponse.Content.ReadAsStringAsync().Result);

                                                    if (purchaseResponse.StatusCode == (System.Net.HttpStatusCode)201)
                                                    {
                                                        if (purchaseResponseBody.gift_code != "")
                                                        {
                                                            ConsoleUtils.Log($"Token: {token} managed to cover nitro classic monthly gift payment for payment count: #{i + 1}");
                                                            ConsoleUtils.Log($"Token: {token} #{i + 1} Nitro Gift: https://discord.gift/{purchaseResponseBody.gift_code}");
                                                        }
                                                        else
                                                            ConsoleUtils.LogError($"Token: {token} failed to send POST to /purchase. Token might be invalid or can't cover payment at this time.");
                                                    }
                                                    else
                                                        ConsoleUtils.LogError($"Token: {token} failed to send POST to /purchase. {purchaseResponse.Content.ReadAsStringAsync().Result}");
                                                }
                                            }
                                        }
                                    }
                                    else
                                        ConsoleUtils.LogError($"Token: {token} failed to send GET to /payment-sources. Token might be invalid.");
                                }
                            }
                            else
                            {
                                ConsoleUtils.Log("Enter Amount: ");
                                int amount = int.Parse(Console.ReadLine());

                                foreach (var token in Tokens)
                                {
                                    HttpClient client = new HttpClient();
                                    client.DefaultRequestHeaders.Add("Authorization", token);
                                    var response = client.GetAsync("https://discord.com/api/v8/users/@me/billing/payment-sources").Result;

                                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                    {
                                        var paymentsources = JsonConvert.DeserializeObject<List<PaymentSource>>(response.Content.ReadAsStringAsync().Result);

                                        if (paymentsources.Count > 0)
                                        {
                                            ConsoleUtils.Log($"Token: {token} has {paymentsources.Count} payment source(s). Trying to purchase {amount} classic yearly gift code(s).");

                                            for (int j = 0; j < paymentsources.Count; j++)
                                            {
                                                for (int i = 0; i < amount; i++)
                                                {
                                                    var purchaseBody = new PurchaseNitroBody()
                                                    {
                                                        gift = true,
                                                        sku_subscription_plan_id = "511651876987469824",
                                                        expected_amount = 4999,
                                                        payment_source_id = paymentsources[j].id
                                                    };

                                                    var purchaseResponse = client.PostAsync("https://discord.com/api/v8/store/skus/521846918637420545/purchase", new StringContent(JsonConvert.SerializeObject(purchaseBody), Encoding.UTF8, "application/json")).Result;
                                                    var purchaseResponseBody = JsonConvert.DeserializeObject<PurchaseNitroResponse>(purchaseResponse.Content.ReadAsStringAsync().Result);

                                                    if (purchaseResponse.StatusCode == (System.Net.HttpStatusCode)201)
                                                    {
                                                        if (purchaseResponseBody.gift_code != "")
                                                        {
                                                            ConsoleUtils.Log($"Token: {token} managed to cover nitro classic yearly gift payment for payment count: #{i + 1}");
                                                            ConsoleUtils.Log($"Token: {token} #{i + 1} Nitro Gift code: https://discord.gift/{purchaseResponseBody.gift_code}");
                                                        }
                                                        else
                                                            ConsoleUtils.LogError($"Token: {token} failed to send POST to /purchase. Token might be invalid or can't cover payment at this time.");
                                                    }
                                                    else
                                                        ConsoleUtils.LogError($"Token: {token} failed to send POST to /purchase. Token might be invalid or can't cover payment at this time with payment method: {paymentsources[j].brand}");
                                                }
                                            }
                                        }
                                    }
                                    else
                                        ConsoleUtils.LogError($"Token: {token} failed to send GET to /payment-sources. Token might be invalid.");
                                }
                            }
                            break;
                        case 2:
                            ConsoleUtils.Log("Enter Duration Option: (1 - Month, 2 - Year)");
                            int duration2 = int.Parse(Console.ReadLine());

                            if (duration2 == 1)
                            {
                                ConsoleUtils.Log("Enter Amount: ");
                                int amount = int.Parse(Console.ReadLine());

                                foreach (var token in Tokens)
                                {
                                    HttpClient client = new HttpClient();
                                    client.DefaultRequestHeaders.Add("Authorization", token);
                                    var response = client.GetAsync("https://discord.com/api/v8/users/@me/billing/payment-sources").Result;

                                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                    {
                                        var paymentsources = JsonConvert.DeserializeObject<List<PaymentSource>>(response.Content.ReadAsStringAsync().Result);

                                        if (paymentsources.Count > 0)
                                        {
                                            ConsoleUtils.Log($"Token: {token} has {paymentsources.Count} payment source(s). Trying to purchase {amount} monthly gift code(s).");

                                            for (int j = 0; j < paymentsources.Count; j++)
                                            {
                                                for (int i = 0; i < amount; i++)
                                                {
                                                    var purchaseBody = new PurchaseNitroBody()
                                                    {
                                                        gift = true,
                                                        sku_subscription_plan_id = "511651880837840896",
                                                        expected_amount = 999,
                                                        payment_source_id = paymentsources[j].id
                                                    };

                                                    var purchaseResponse = client.PostAsync("https://discord.com/api/v8/store/skus/521847234246082599/purchase", new StringContent(JsonConvert.SerializeObject(purchaseBody), Encoding.UTF8, "application/json")).Result;
                                                    var purchaseResponseBody = JsonConvert.DeserializeObject<PurchaseNitroResponse>(purchaseResponse.Content.ReadAsStringAsync().Result);

                                                    if (purchaseResponse.StatusCode == (System.Net.HttpStatusCode)201)
                                                    {
                                                        if (purchaseResponseBody.gift_code != "")
                                                        {
                                                            ConsoleUtils.Log($"Token: {token} managed to cover nitro monthly gift payment for payment count: #{i + 1}");
                                                            ConsoleUtils.Log($"Token: {token} #{i + 1} Nitro Gift: https://discord.gift/{purchaseResponseBody.gift_code}");
                                                        }
                                                        else
                                                            ConsoleUtils.LogError($"Token: {token} failed to send POST to /purchase. Token might be invalid or can't cover payment at this time.");
                                                    }
                                                    else
                                                        ConsoleUtils.LogError($"Token: {token} failed to send POST to /purchase. {purchaseResponse.Content.ReadAsStringAsync().Result}");
                                                }
                                            }
                                        }
                                    }
                                    else
                                        ConsoleUtils.LogError($"Token: {token} failed to send GET to /payment-sources. Token might be invalid.");
                                }
                            }
                            else
                            {
                                ConsoleUtils.Log("Enter Amount: ");
                                int amount = int.Parse(Console.ReadLine());

                                foreach (var token in Tokens)
                                {
                                    HttpClient client = new HttpClient();
                                    client.DefaultRequestHeaders.Add("Authorization", token);
                                    var response = client.GetAsync("https://discord.com/api/v8/users/@me/billing/payment-sources").Result;

                                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                    {
                                        var paymentsources = JsonConvert.DeserializeObject<List<PaymentSource>>(response.Content.ReadAsStringAsync().Result);

                                        if (paymentsources.Count > 0)
                                        {
                                            ConsoleUtils.Log($"Token: {token} has {paymentsources.Count} payment source(s). Trying to purchase {amount} yearly gift code(s).");

                                            for (int j = 0; j < paymentsources.Count; j++)
                                            {
                                                for (int i = 0; i < amount; i++)
                                                {
                                                    var purchaseBody = new PurchaseNitroBody()
                                                    {
                                                        gift = true,
                                                        sku_subscription_plan_id = "511651885459963904",
                                                        expected_amount = 9999,
                                                        payment_source_id = paymentsources[j].id
                                                    };

                                                    var purchaseResponse = client.PostAsync("https://discord.com/api/v8/store/skus/521847234246082599/purchase", new StringContent(JsonConvert.SerializeObject(purchaseBody), Encoding.UTF8, "application/json")).Result;
                                                    var purchaseResponseBody = JsonConvert.DeserializeObject<PurchaseNitroResponse>(purchaseResponse.Content.ReadAsStringAsync().Result);

                                                    if (purchaseResponse.StatusCode == (System.Net.HttpStatusCode)201)
                                                    {
                                                        if (purchaseResponseBody.gift_code != "")
                                                        {
                                                            ConsoleUtils.Log($"Token: {token} managed to cover nitro yearly gift payment for payment count: #{i + 1}");
                                                            ConsoleUtils.Log($"Token: {token} #{i + 1} Nitro Gift code: https://discord.gift/{purchaseResponseBody.gift_code}");
                                                        }
                                                        else
                                                            ConsoleUtils.LogError($"Token: {token} failed to send POST to /purchase. Token might be invalid or can't cover payment at this time.");
                                                    }
                                                    else
                                                        ConsoleUtils.LogError($"Token: {token} failed to send POST to /purchase. Token might be invalid or can't cover payment at this time with payment method: {paymentsources[j].brand}");
                                                }
                                            }
                                        }
                                    }
                                    else
                                        ConsoleUtils.LogError($"Token: {token} failed to send GET to /payment-sources. Token might be invalid.");
                                }
                            }
                            break;
                        case 3:
                            ConsoleUtils.Log("Enter Invite Code: ");
                            string invite = Console.ReadLine();

                            ConsoleUtils.Log("Amount: ");
                            int amountofBoosts = int.Parse(Console.ReadLine());

                            foreach(var token in Tokens)
                            {
                                HttpClient client = new HttpClient();
                                client.DefaultRequestHeaders.Add("Authorization", token);

                                var joinResponse = client.PostAsync($"https://discord.com/api/v8/invites/{invite}", new StringContent("{}", Encoding.UTF8, "application/json"));
                                var guildResponse = JsonConvert.DeserializeObject<JoinGuildResponse>(joinResponse.Result.Content.ReadAsStringAsync().Result);

                                if (joinResponse.Result.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    var response = client.GetAsync("https://discord.com/api/v8/users/@me/guilds/premium/subscription-slots").Result;

                                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                    {
                                        var premiumsubscriptions = JsonConvert.DeserializeObject<List<PremiumSubscriptionSlot>>(response.Content.ReadAsStringAsync().Result);

                                        if (premiumsubscriptions.Count > 0)
                                        {
                                            ConsoleUtils.Log($"Token: {token} has {premiumsubscriptions.Count} subscription slot(s). Trying to boost https://discord.gg/{invite}");
                                            int boosted = 0;

                                            foreach(var premiumsub in premiumsubscriptions)
                                            {
                                                if (!premiumsub.canceled && premiumsub.premium_guild_subscription == null)
                                                {
                                                    if (boosted != amountofBoosts)
                                                    {
                                                        var boostBody = new BoostGuildBody()
                                                        {
                                                            user_premium_guild_subscription_slot_ids = new string[]
                                                            {
                                                                premiumsub.id
                                                            }
                                                        };

                                                        var boostResponse = client.PutAsync($"https://discord.com/api/v8/guilds/{guildResponse.guild.id}/premium/subscriptions", new StringContent(JsonConvert.SerializeObject(boostBody), Encoding.UTF8, "application/json")).Result;

                                                        if (boostResponse.StatusCode == (System.Net.HttpStatusCode)201)
                                                        {
                                                            boosted++;
                                                            ConsoleUtils.Log($"Token: {token} boosted {guildResponse.guild.name} (discord.gg/{invite}) with spare boost!");
                                                        }
                                                        else
                                                        {
                                                            if (boostResponse.Content.ReadAsStringAsync().Result == "{\"message\": \"Maximum number of premium guild subscriptions reached.\", \"code\": 30026}")
                                                                ConsoleUtils.LogError($"Token: {token} Maximum number of guild subscriptions reached.");
                                                            else
                                                                ConsoleUtils.LogError($"Token: {token} failed to send PUT to /premium/subscriptions. Token might be invalid or guild might be invalid.");
                                                        }
                                                    }
                                                }
                                            }

                                            if (boosted != amountofBoosts)
                                            {
                                                ConsoleUtils.Log($"Token: {token} doesn't have enough boosts to cover for the server. Buying required boosts.");
                                                
                                                var buyBody = new PremiumGuildSubscriptionBody()
                                                {
                                                    additional_plans = new Additional_Plans[]
                                                    {
                                                        new Additional_Plans()
                                                        {
                                                            plan_id = "590665532894740483",
                                                            quantity = amountofBoosts - boosted
                                                        }
                                                    }
                                                };

                                                var buyResponse = client.PatchAsync($"https://discord.com/api/v8/users/@me/billing/subscriptions/{premiumsubscriptions[0].subscription_id}", new StringContent(JsonConvert.SerializeObject(buyBody), Encoding.UTF8, "application/json"));
                                                
                                                if (buyResponse.Result.StatusCode == System.Net.HttpStatusCode.OK)
                                                {
                                                    ConsoleUtils.Log($"Token: {token} has covered {amountofBoosts - boosted} boost(s) by purchasing with payment method.");
                                                    boosted = amountofBoosts;
                                                    var response3 = client.GetAsync("https://discord.com/api/v8/users/@me/guilds/premium/subscription-slots").Result;

                                                    if (response3.StatusCode == System.Net.HttpStatusCode.OK)
                                                    {
                                                        var premiumsubscriptions2 = JsonConvert.DeserializeObject<List<PremiumSubscriptionSlot>>(response3.Content.ReadAsStringAsync().Result);

                                                        if (premiumsubscriptions2.Count > 0)
                                                        {
                                                            foreach (var premiumsub in premiumsubscriptions2)
                                                            {
                                                                ConsoleUtils.Log($"Token: {token} has {premiumsubscriptions.Count} subscription slot(s). Trying to boost https://discord.gg/{invite}");
                                                                if (!premiumsub.canceled && premiumsub.premium_guild_subscription == null)
                                                                {
                                                                    if (boosted != amountofBoosts)
                                                                    {
                                                                        var boostBody = new BoostGuildBody()
                                                                        {
                                                                            user_premium_guild_subscription_slot_ids = new string[]
                                                                            {
                                                                                premiumsub.id
                                                                            }
                                                                        };

                                                                        var boostResponse = client.PutAsync($"https://discord.com/api/v8/guilds/{guildResponse.guild.id}/premium/subscriptions", new StringContent(JsonConvert.SerializeObject(boostBody), Encoding.UTF8, "application/json")).Result;

                                                                        if (boostResponse.StatusCode == (System.Net.HttpStatusCode)201)
                                                                        {
                                                                            boosted++;
                                                                            ConsoleUtils.Log($"Token: {token} boosted {guildResponse.guild.name} (discord.gg/{invite}) with spare boost!");
                                                                        }
                                                                        else
                                                                        {
                                                                            if (boostResponse.Content.ReadAsStringAsync().Result == "{\"message\": \"Maximum number of premium guild subscriptions reached.\", \"code\": 30026}")
                                                                                ConsoleUtils.LogError($"Token: {token} Maximum number of guild subscriptions reached.");
                                                                            else
                                                                                ConsoleUtils.LogError($"Token: {token} failed to send PUT to /premium/subscriptions. Token might be invalid or guild might be invalid.");
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                        ConsoleUtils.LogError($"Token: {token} failed to send GET to /subscription-slots. Token might be invalid.");
                                                }
                                                else
                                                    ConsoleUtils.Log($"Token: {token} failed to cover payment for amount of required boosts.");
                                            }

                                            var settingsBody = new GuildSettingsPatch()
                                            {
                                                muted = true,
                                                suppress_everyone = true,
                                                suppress_roles = true,
                                                restricted_guilds = new string[]
                                                {
                                                    guildResponse.guild.id
                                                }
                                            };

                                            client.PatchAsync($"https://discord.com/api/v8/users/@me/guilds/{guildResponse.guild.id}/settings", new StringContent(JsonConvert.SerializeObject(settingsBody), Encoding.UTF8, "application/json"));

                                        }
                                    }
                                    else
                                        ConsoleUtils.LogError($"Token: {token} failed to send GET to /subscription-slots. Token might be invalid.");
                                }
                                else
                                {
                                    ConsoleUtils.LogError(joinResponse.Result.Content.ReadAsStringAsync().Result);
                                    ConsoleUtils.LogError($"Token: {token} failed to send POST to /invites/{invite}. Token might be invalid or invite might be invalid.");
                                }
                            }
                            break;
                    }
                }
            }
            catch(Exception e) {
                ConsoleUtils.LogError($"An exception occurred! Exception: {e.ToString()}");
            }

            Console.ReadKey();
        }
    }
}
