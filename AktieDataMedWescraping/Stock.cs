using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AktieDataMedWescraping
{
    internal class Stock
    {

        public string Name { get; set; }
        public string changeInPercent { get; set; }
        public double LastTrade { get; set; }
        public double BuyPrice { get; set; }
        public double SellPrice { get; set; }


        //Small hobby project where you can scrape stock data from digital brokers.In this project, i scraped the data from a couple of Swedish brokers, Avanza bank and Nordnet. The scraping is working, hope you're enjoying it! 


        //The scraper is getting the list for the OMXS30 where we find the the top 30 best turnover stocks from the Stockholm exchange/Nasdaq Stockholm. It is then showing the top 30 stocks with the scraper and the top 3 best performanced stocks and the top 3 bad performed stocks.

        //I removed the URL for the digital bank i was scraping, so change the URL and configure the HtmlNodes. Have in mind that you cant use this on commercial purpose, always check with the bank ;) 


    }
}
