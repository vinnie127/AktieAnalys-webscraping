using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AktieDataMedWescraping
{
    public class CrawlerDI
    {



        public async Task StartCrawlerAsync()
        {

            var url = "PLACE UR URL HERE";

            var httpClient = new HttpClient();

            var html = await httpClient.GetStringAsync(url);


            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var divs = htmlDocument.DocumentNode.Descendants("div").Where(n => n.GetAttributeValue("class", "").Equals("Flexbox__StyledFlexbox-sc-1ob4g1e-0 eYavUv Row__StyledRow-sc-1iamenj-0 foFHXj Rows__AlignedRow-sc-1udgki9-0 dnLFDN")).ToList();
            var stocks = new List<Stock>();


            var ProductsList = htmlDocument.DocumentNode.SelectNodes("//div[@class='Flexbox__StyledFlexbox-sc-1ob4g1e-0 eYavUv Row__StyledRow-sc-1iamenj-0 foFHXj Rows__AlignedRow-sc-1udgki9-0 dnLFDN']");




            foreach (var div in ProductsList)
            {
                var StockPercentChange = div.SelectSingleNode(".//div[@class='Flexbox__StyledFlexbox-sc-1ob4g1e-0 eDgWiq Cell__StyledFlexbox-sc-icfddc-0 dvPBCt']");
                var StockLastPrice = div.SelectSingleNode(".//div[@class='Flexbox__StyledFlexbox-sc-1ob4g1e-0 dcnKuH Cell__StyledFlexbox-sc-icfddc-0 dvPBCt NumberCell__StyledFlexTableCell-sc-icuiuz-0 hlZdvv']");
                var BuyPrice = div.SelectSingleNode(".//div[@class='Flexbox__StyledFlexbox-sc-1ob4g1e-0 imQKMv Cell__StyledFlexbox-sc-icfddc-0 dvPBCt NumberCell__StyledFlexTableCell-sc-icuiuz-0 hlZdvv']");
                var SellPrice = div.SelectSingleNode(".//div[@class='Flexbox__StyledFlexbox-sc-1ob4g1e-0 imQKMv Cell__StyledFlexbox-sc-icfddc-0 dvPBCt NumberCell__StyledFlexTableCell-sc-icuiuz-0 hlZdvv']");




                    var stock = new Stock()
                    {
                        Name = div.Descendants("a").Where(a => a.GetAttributeValue("class", "").Equals("Link__StyledLink-sc-apj04t-0 foCaAq NameCell__StyledLink-sc-qgec4s-0 hZYbiE")).FirstOrDefault().InnerText,         
                       changeInPercent = StockPercentChange.Descendants("span").FirstOrDefault().InnerText,
                        LastTrade = double.Parse(StockLastPrice.Descendants("span").FirstOrDefault().InnerText),
                        BuyPrice = double.Parse(BuyPrice.Descendants("span").FirstOrDefault().InnerText),
                        SellPrice = double.Parse(SellPrice.Descendants("span").FirstOrDefault().InnerText)
                    };

                    stocks.Add(stock);
                

            }
             
            foreach (var stock in stocks)
            {
                Console.Write("Stock : " +  stock.Name + " | ");
                Console.Write("Today change :  "   + stock.changeInPercent + " | ");
                Console.Write(" LastTrade: " + stock.LastTrade+" SEK" + " | ");
                Console.Write(" Buy: " + stock.BuyPrice + " SEK" + " | ");
                Console.Write(" Sell: " + stock.SellPrice + " SEK" + " | ");
                Console.WriteLine();

                

            }

            Console.WriteLine("");
            Console.WriteLine("Top 3 bad performance today Stocks: ");
           var Top3Stocks =  stocks.OrderByDescending(s => s.changeInPercent).Take(3).ToList();
            foreach (var item in Top3Stocks)
            {

                Console.WriteLine(item.Name + ": " + item.changeInPercent);

            }


            double changeInPercent;
            var positiveChangeStocks = stocks.Where(s => double.TryParse(s.changeInPercent.Replace("%", ""), out changeInPercent) && changeInPercent > 0);
            var Top3BestStocks = positiveChangeStocks.OrderByDescending(s => double.TryParse(s.changeInPercent.Replace("%", ""), out changeInPercent) ? changeInPercent : 0).Take(3).ToList();



            Console.WriteLine();
            Console.WriteLine("Top 3 Best stocks today");
            foreach (var item in Top3BestStocks)
            {
                Console.WriteLine(item.Name + ": " + item.changeInPercent);
            }


        }



    }
}
