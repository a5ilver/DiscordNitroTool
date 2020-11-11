using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Utils;
using DiscordNitroTool.JSON;
using DiscordNitroTool.Utils;
using System.Threading;

namespace DiscordNitroTool
{
    class Program
    {
        static List<string> Tokens = new List<string>();

        static void Main(string[] args)
        {
            ConsoleUtils.SetTitle("Discord Nitro Tool - Yaekith and tostring");
            ConsoleUtils.LogV2("[DISCLAIMER] I AM NOT RESPONSIBLE FOR ANY DAMAGE YOU CAUSE WHILE USING THIS PROGRAM, PLEASE USE THIS PROGRAM WITH YOUR OWN TOKENS. DO NOT USE IT WITH OTHER PEOPLES TOKENS. YOU HAVE BEEN WARNED FOR ANY POTENTIAL LEGAL CONSEQUENCES BECAUSE OF THIS PROGRAM.");
            ConsoleUtils.LogV2(@" 
              ███▄,                              ,╓╖╓,
             ╙█████░▒▄'                      ▄███▒▓▓╣╣╢╗
                ▀▀█████▄,:                  ▐█████████▓▓▓▄
                    ` █▓░  █╖               ████████▓▄▓▀██
                      '██▄▄█▓▒╗,             ███████▀▀▀██▀
                        ▀█████▓▒╢╖            ▀██▄▄░░░░sT
                          ██████▓╬╢╖          └███░░░░░─
                           ▀█████▓╣╣▒╣         ███▄µ▒░░
                             ▀████▓▓▓▓╫┐       ,██▀▀
                               ████▓▓█▓▌@▓▒▓▒▄@▓█▓µ▌\  ┌╖
                                ▐███████▓▓▓▓▓▒▄▌╢╢╣▀▒ ,░░░▒▒∩,,
                                 ▀███████▓█▓▓╩▒▀█▄▒▒▒▄▒░░░|░▒░░]▒
                                    ▀████████▓▓▓▒▀███Ñ▓▒░▒░░░░░▒▒▒
                                     ░`▀██████▓▓▓@▒███▒▒▒▒▀▒¢g▄╢▒░
                                        ▐█████▓▓▓▓╣▓▒██▌▒▒▒▒▒▒▐███▄
                                       ░ ██████▓▓▓▓▌Ñ▓▓▀██▄▒╢¼████æ▄
                                         ▀███████▓███▓▓▓▓█░░░░▀▀▀▀██▄
                                        ╒█████████▓██▓▓▓▓█▒▒▒▒▒▒▒▒▒░░▄
                                       ,▓█╣▓██▓▓███████▓▓█▒▒▒╢╢▒▒▒▒▒▒░
                                      ▄▓█▓▄▓███▓█████████████▄▒╢▓╣▓▓╜
                                     ▐████▓▓██▓▓▓▓▒▒╢╣╣▒▒▒▀█▀█-└▀▀
                                    https://exploiting-discord-for.fun");

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
                    ConsoleUtils.SetTitle($"Discord Nitro Tool - Yaekith and tostring - {Tokens.Count} Token(s)");
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

                                new Thread(() =>
                                {
                                    for(var t = 0; t < Tokens.Count(); t++)
                                    {
                                        var token = Tokens[t];

                                        HttpClient client = new HttpClient();
                                        client.DefaultRequestHeaders.Add("Authorization", token);
                                        var response = client.GetAsync("https://discord.com/api/v8/users/@me/billing/payment-sources").Result;

                                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                        {
                                            var paymentsources = JsonConvert.DeserializeObject<List<PaymentSource>>(response.Content.ReadAsStringAsync().Result);

                                            if (paymentsources.Count > 0)
                                            {
                                                ConsoleUtils.Log($"Token #{t}: Trying to purchase {amount} classic monthly gift code(s).");

                                                for (int i = 0; i < amount; i++)
                                                {
                                                    var purchaseBody = new PurchaseNitroBody()
                                                    {
                                                        gift = true,
                                                        sku_subscription_plan_id = "511651871736201216",
                                                        expected_amount = 499,
                                                        payment_source_id = paymentsources[0].id
                                                    };

                                                    var purchaseResponse = client.PostAsync("https://discord.com/api/v8/store/skus/521846918637420545/purchase", new StringContent(JsonConvert.SerializeObject(purchaseBody), Encoding.UTF8, "application/json")).Result;
                                                    var purchaseResponseBody = JsonConvert.DeserializeObject<PurchaseNitroResponse>(purchaseResponse.Content.ReadAsStringAsync().Result);

                                                    if (purchaseResponse.StatusCode == (System.Net.HttpStatusCode)201)
                                                    {
                                                        if (purchaseResponseBody.gift_code != "")
                                                            ConsoleUtils.Log($"Token #{t}: #{i + 1} Nitro Gift: https://discord.gift/{purchaseResponseBody.gift_code}");
                                                        else
                                                        {
                                                            ConsoleUtils.LogError($"Token #{t}: failed to send POST to /purchase.");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ConsoleUtils.LogError($"Token #{t}: failed to send POST to /purchase.");
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ConsoleUtils.LogError($"Token #{t}: failed to send GET to /payment-sources.");
                                        }
                                    }
                                }).Start();
                            }
                            else
                            {
                                ConsoleUtils.Log("Enter Amount: ");
                                int amount = int.Parse(Console.ReadLine());

                                new Thread(() =>
                                {
                                    for(var t = 0; t < Tokens.Count(); t++)
                                    {
                                        var token = Tokens[t];

                                        HttpClient client = new HttpClient();
                                        client.DefaultRequestHeaders.Add("Authorization", token);
                                        var response = client.GetAsync("https://discord.com/api/v8/users/@me/billing/payment-sources").Result;

                                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                        {
                                            var paymentsources = JsonConvert.DeserializeObject<List<PaymentSource>>(response.Content.ReadAsStringAsync().Result);

                                            if (paymentsources.Count > 0)
                                            {
                                                ConsoleUtils.Log($"Token #{t}: Trying to purchase {amount} classic yearly gift code(s).");

                                                for (int i = 0; i < amount; i++)
                                                {
                                                    var purchaseBody = new PurchaseNitroBody()
                                                    {
                                                        gift = true,
                                                        sku_subscription_plan_id = "511651876987469824",
                                                        expected_amount = 4999,
                                                        payment_source_id = paymentsources[0].id
                                                    };

                                                    var purchaseResponse = client.PostAsync("https://discord.com/api/v8/store/skus/521846918637420545/purchase", new StringContent(JsonConvert.SerializeObject(purchaseBody), Encoding.UTF8, "application/json")).Result;
                                                    var purchaseResponseBody = JsonConvert.DeserializeObject<PurchaseNitroResponse>(purchaseResponse.Content.ReadAsStringAsync().Result);

                                                    if (purchaseResponse.StatusCode == (System.Net.HttpStatusCode)201)
                                                    {
                                                        if (purchaseResponseBody.gift_code != "")
                                                            ConsoleUtils.Log($"Token #{t}: #{i + 1} Nitro Gift code: https://discord.gift/{purchaseResponseBody.gift_code}");
                                                        else
                                                        {
                                                            ConsoleUtils.LogError($"Token #{t}: failed to send POST to /purchase.");

                                                        }
                                                    }
                                                    else
                                                    {
                                                        ConsoleUtils.LogError($"Token #{t}: failed to send POST to /purchase.");

                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ConsoleUtils.LogError($"Token #{t}: failed to send GET to /payment-sources.");

                                        }
                                    }
                                }).Start();
                            }
                            break;
                        case 2:
                            ConsoleUtils.Log("Enter Duration Option: (1 - Month, 2 - Year)");
                            int duration2 = int.Parse(Console.ReadLine());

                            if (duration2 == 1)
                            {
                                ConsoleUtils.Log("Enter Amount: ");
                                int amount = int.Parse(Console.ReadLine());

                                new Thread(() =>
                                {
                                    for(var t = 0; t < Tokens.Count(); t++)
                                    {
                                        var token = Tokens[t];

                                        HttpClient client = new HttpClient();
                                        client.DefaultRequestHeaders.Add("Authorization", token);
                                        var response = client.GetAsync("https://discord.com/api/v8/users/@me/billing/payment-sources").Result;

                                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                        {
                                            var paymentsources = JsonConvert.DeserializeObject<List<PaymentSource>>(response.Content.ReadAsStringAsync().Result);

                                            if (paymentsources.Count > 0)
                                            {
                                                ConsoleUtils.Log($"Token #{t}: Trying to purchase {amount} monthly gift code(s).");

                                                for (int i = 0; i < amount; i++)
                                                {
                                                    var purchaseBody = new PurchaseNitroBody()
                                                    {
                                                        gift = true,
                                                        sku_subscription_plan_id = "511651880837840896",
                                                        expected_amount = 999,
                                                        payment_source_id = paymentsources[0].id
                                                    };

                                                    var purchaseResponse = client.PostAsync("https://discord.com/api/v8/store/skus/521847234246082599/purchase", new StringContent(JsonConvert.SerializeObject(purchaseBody), Encoding.UTF8, "application/json")).Result;
                                                    var purchaseResponseBody = JsonConvert.DeserializeObject<PurchaseNitroResponse>(purchaseResponse.Content.ReadAsStringAsync().Result);

                                                    if (purchaseResponse.StatusCode == (System.Net.HttpStatusCode)201)
                                                    {
                                                        if (purchaseResponseBody.gift_code != "")
                                                            ConsoleUtils.Log($"Token #{t}: #{i + 1} Nitro Gift: https://discord.gift/{purchaseResponseBody.gift_code}");
                                                        else
                                                        {
                                                            ConsoleUtils.LogError($"Token #{t}: failed to send POST to /purchase.");

                                                        }
                                                    }
                                                    else
                                                    {
                                                        ConsoleUtils.LogError($"Token #{t}: failed to send POST to /purchase.");

                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ConsoleUtils.LogError($"Token #{t}: failed to send GET to /payment-sources.");

                                        }
                                    }
                                }).Start();
                            }
                            else
                            {
                                ConsoleUtils.Log("Enter Amount: ");
                                int amount = int.Parse(Console.ReadLine());
                                
                                new Thread(() =>
                                {
                                    for(var t = 0; t < Tokens.Count(); t++)
                                    {
                                        var token = Tokens[t];

                                        HttpClient client = new HttpClient();
                                        client.DefaultRequestHeaders.Add("Authorization", token);
                                        var response = client.GetAsync("https://discord.com/api/v8/users/@me/billing/payment-sources").Result;

                                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                        {
                                            var paymentsources = JsonConvert.DeserializeObject<List<PaymentSource>>(response.Content.ReadAsStringAsync().Result);

                                            if (paymentsources.Count > 0)
                                            {
                                                ConsoleUtils.Log($"Token #{t}: Trying to purchase {amount} yearly gift code(s).");

                                                for (int i = 0; i < amount; i++)
                                                {
                                                    var purchaseBody = new PurchaseNitroBody()
                                                    {
                                                        gift = true,
                                                        sku_subscription_plan_id = "511651885459963904",
                                                        expected_amount = 9999,
                                                        payment_source_id = paymentsources[0].id
                                                    };

                                                    var purchaseResponse = client.PostAsync("https://discord.com/api/v8/store/skus/521847234246082599/purchase", new StringContent(JsonConvert.SerializeObject(purchaseBody), Encoding.UTF8, "application/json")).Result;
                                                    var purchaseResponseBody = JsonConvert.DeserializeObject<PurchaseNitroResponse>(purchaseResponse.Content.ReadAsStringAsync().Result);

                                                    if (purchaseResponse.StatusCode == (System.Net.HttpStatusCode)201)
                                                    {
                                                        if (purchaseResponseBody.gift_code != "")
                                                            ConsoleUtils.Log($"Token #{t}: #{i + 1} Nitro Gift code: https://discord.gift/{purchaseResponseBody.gift_code}");
                                                        else
                                                        {
                                                            ConsoleUtils.LogError($"Token #{t}: failed to send POST to /purchase.");

                                                        }
                                                    }
                                                    else
                                                    {
                                                        ConsoleUtils.LogError($"Token #{t}: failed to send POST to /purchase.");

                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ConsoleUtils.LogError($"Token #{t}: failed to send GET to /payment-sources.");

                                        }
                                    }
                                }).Start();
                            }
                            break;
                        case 3:
                            ConsoleUtils.Log("Enter Invite Code: ");
                            string invite = Console.ReadLine();

                            ConsoleUtils.Log("Amount: ");
                            int amountofBoosts = int.Parse(Console.ReadLine());
                            
                            var unlimited = false;

                            if (amountofBoosts == -1)
                                unlimited = true;

                            new Thread(() =>
                            {
                                for(var t = 0; t < Tokens.Count(); t++)
                                {
                                    var token = Tokens[t];

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
                                                ConsoleUtils.Log($"Token #{t} has {premiumsubscriptions.Count} subscription slot(s). Trying to boost https://discord.gg/{invite}");
                                                int boosted = 0;
                                                int count = 0;

                                                foreach (var premiumsub in premiumsubscriptions)
                                                {
                                                    count++;

                                                    if (premiumsub.canceled)
                                                    {
                                                        client.PostAsync($"https://discord.com/api/v8/users/@me/guilds/premium/subscription-slots/{premiumsub.id}/uncancel", new StringContent("{}", Encoding.UTF8, "application/json"));
                                                        ConsoleUtils.Log($"Uncancelled boost for subscription ID: {premiumsub.id}");
                                                    }

                                                    if (premiumsub.cooldown_ends_at != null && premiumsub.CanUseBoost())
                                                    {
                                                        if (unlimited)
                                                        {
                                                            if (premiumsub.premium_guild_subscription != null)
                                                                client.DeleteAsync($"https://discord.com/api/v8/guilds/{premiumsub.premium_guild_subscription.guild_id}/premium/subscriptions/{premiumsub.premium_guild_subscription.id}");


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
                                                                ConsoleUtils.Log($"Token #{t}: boosted {guildResponse.guild.name} (discord.gg/{invite}) with spare boost!");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (boosted != amountofBoosts)
                                                            {
                                                                if (premiumsub.premium_guild_subscription != null)
                                                                    client.DeleteAsync($"https://discord.com/api/v8/guilds/{premiumsub.premium_guild_subscription.guild_id}/premium/subscriptions/{premiumsub.premium_guild_subscription.id}");


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
                                                                    ConsoleUtils.Log($"Token #{t}: boosted {guildResponse.guild.name} (discord.gg/{invite}) with spare boost!");
                                                                }
                                                            }
                                                        }
                                                    }
                                                }

                                                if (!unlimited)
                                                {
                                                    if (boosted != amountofBoosts)
                                                    {
                                                        ConsoleUtils.Log($"Token #{t}: doesn't have enough boosts to cover for the server. Buying required boosts.");

                                                        var subscriptionSlotsRequest = client.GetAsync("https://discord.com/api/v8/users/@me/guilds/premium/subscription-slots").Result;

                                                        if (subscriptionSlotsRequest.StatusCode == System.Net.HttpStatusCode.OK)
                                                        {
                                                            var subscriptionSlots = JsonConvert.DeserializeObject<List<PremiumSubscriptionSlot>>(subscriptionSlotsRequest.Content.ReadAsStringAsync().Result);

                                                            var premiumGuildSubscriptionBuyBody = new PremiumGuildSubscriptionBody()
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

                                                            var billingSubscriptionPatchRequest = client.PatchAsync($"https://discord.com/api/v8/users/@me/billing/subscriptions/{subscriptionSlots[0].subscription_id}", new StringContent(JsonConvert.SerializeObject(premiumGuildSubscriptionBuyBody), Encoding.UTF8, "application/json"));

                                                            foreach (var sub in subscriptionSlots)
                                                            {
                                                                if (sub.cooldown_ends_at != null)
                                                                {
                                                                    var boostBody = new BoostGuildBody()
                                                                    {
                                                                        user_premium_guild_subscription_slot_ids = new string[]
                                                                        {
                                                                            sub.id
                                                                        }
                                                                    };

                                                                    var subscriptionsPutRequest = client.PutAsync($"https://discord.com/api/v8/guilds/{guildResponse.guild.id}/premium/subscriptions", new StringContent(JsonConvert.SerializeObject(boostBody), Encoding.UTF8, "application/json"));

                                                                    if (subscriptionsPutRequest.Result.StatusCode == System.Net.HttpStatusCode.OK)
                                                                    {
                                                                        boosted++;
                                                                        ConsoleUtils.Log($"Token #{t}: boosted {guildResponse.guild.name} (discord.gg/{invite}) with spare boost!");
                                                                    }
                                                                }
 
                                                            }
                                                        }
                                                        else
                                                            ConsoleUtils.LogError($"Token: {token}: failed to send GET to /subscription-slots");
                                                    }
                                                }

                                                var settingsBody = new GuildSettingsPatch()
                                                {
                                                    muted = true,
                                                    suppress_everyone = true,
                                                    suppress_roles = true
                                                };

                                                var guildDms = new GuildSettingsPatch()
                                                {
                                                    restricted_guilds = new string[]
                                                    {
                                                            guildResponse.guild.id
                                                    }
                                                };

                                                client.PatchAsync($"https://discord.com/api/v8/users/@me/guilds/{guildResponse.guild.id}/settings", new StringContent(JsonConvert.SerializeObject(settingsBody), Encoding.UTF8, "application/json"));

                                                client.PatchAsync($"https://discord.com/api/v8/users/@me/settings", new StringContent(JsonConvert.SerializeObject(guildDms), Encoding.UTF8, "application/json"));
                                            }
                                        }
                                        else
                                        {
                                            ConsoleUtils.LogError($"Token #{t}: failed to send GET to /subscription-slots.");
                                        }
                                    }
                                    else
                                    {
                                        ConsoleUtils.LogError($"Token #{t}: failed to send POST to /invites/{invite}.");
                                    }
                                }
                            }).Start();
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
