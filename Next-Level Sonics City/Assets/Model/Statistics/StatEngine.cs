using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Statistics
{
    public class StatEngine : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            StatReport a = new StatReport();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private List<StatReport> _reports;
        public int Year { get; }
        public int Quarter { get; }

        private const int STARTYEAR = 2020;

        public StatEngine()
        {
            _reports = new List<StatReport>();
            Year = STARTYEAR;
            Quarter = 0;
        }

        //public float CalculateTax(Person person)
        //{
        //    //TODO
        //    return 0;
        //}

        //public float CalculatePersonHappiness(Person person)
        //{
        //    //TODO
        //    return 0;
        //}

        //public float CalculateBuildingHappiness(IBuilding building)
        //{
        //    //TODO
        //    return 0;
        //}

        //public float CalculateCityHappiness(List<IBuilding> buildings)
        //{
        //    //TODO
        //    return 0;
        //}

        //public int sumMaintainance(List<IBuilding> buildings)
        //{
        //    //TODO
        //    return 0;
        //}

        public int GetElectricityProduced()
        {
            //TODO
            throw new NotImplementedException();
        }

        public int GetElectricityConsumed()
        {
            //TODO
            throw new NotImplementedException();
        }

        public StatReport GetStatisticsReport()
        {
            return _reports[_reports.Count - 1];
        }

        public List<StatReport> GetLastGivenStatisticsReport(int index)
        {
            List<StatReport> reports = new List<StatReport>(index);

            int length = _reports.Count - 1;

            for (int i = 0; i < index; ++i)
            {
                reports[i] = _reports[length - i];
            }

            return reports;
        }

        public float GetCommercialToIndustrialRate()
        {
            //TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// Records the expense of the building
        /// </summary>
        /// <param name="price">positive if expense and negative if income</param>
        /// <exception cref="NotImplementedException"></exception>

        public void SumBuildingPrice(int price)
        {
            //TODO
            throw new NotImplementedException();
        }

        /// <summary>
        /// Records the income of destruction
        /// </summary>
        /// <param name="price">positive if income and negative if expense</param>
        /// <exception cref="NotImplementedException"></exception>
        public void SumDestroyPrice(int price)
        {
            //TODO
            throw new NotImplementedException();
        }

        public void nextQuarter()
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}