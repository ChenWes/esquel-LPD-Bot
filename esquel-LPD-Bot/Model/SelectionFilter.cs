using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esquel_LPD_Bot.Model
{
    public class SelectionFilter
    {
        public const string FilterTypeAnd = "AND";
        public const string FilterTypeOr = "OR";
        public const string FilterTypeLeaf = "LEAF";

        public String FilterType { get; set; }
        public SelectionFilter[] Filters { get; set; }

        public string AttributeName { get; set; }
        public string SearchOperator { get; set; }
        public string FilterValue { get; set; }

        public SelectionFilter()
        {

        }        

        //public override string ToString()
        //{
        //    if (this.FilterType == FilterTypeLeaf)
        //    {
        //        return AttributeName + " " + SearchOperator + " " + FilterValue;
        //    }
        //    else
        //    {
        //        string sSeparator = "";

        //        switch (FilterType)
        //        {
        //            case SelectionFilter.FilterTypeAnd:
        //                sSeparator = " and ";
        //                break;
        //            case SelectionFilter.FilterTypeOr:
        //                sSeparator = " or ";
        //                break;
        //        }

        //        string[] sElement = new string[Filters.Length];
        //        for (int i = 0; i < Filters.Length; i++)
        //        {
        //            sElement[i] = Filters[i].ToString();
        //        }
        //        return "(" + string.Join(sSeparator, sElement) + ")";
        //    }
        //}

        public static SelectionFilter CreateAndFilter(SelectionFilter[] filters)
        {
            SelectionFilter returnValue = new SelectionFilter();

            returnValue.FilterType = SelectionFilter.FilterTypeAnd;
            returnValue.Filters = filters;

            return returnValue;
        }

        public static SelectionFilter CreateOrFilter(SelectionFilter[] filters)
        {
            SelectionFilter returnValue = new SelectionFilter();

            returnValue.FilterType = SelectionFilter.FilterTypeOr;
            returnValue.Filters = filters;

            return returnValue;
        }

        public static SelectionFilter CreateLeaf(string attributeName, string searchOperator, string filterValue)
        {
            SelectionFilter returnValue = new SelectionFilter();

            returnValue.FilterType = SelectionFilter.FilterTypeLeaf;
            returnValue.AttributeName = attributeName;
            returnValue.SearchOperator = searchOperator;
            returnValue.FilterValue = filterValue;

            return returnValue;
        }
    }
}