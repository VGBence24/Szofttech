using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Statistics
{
    public class StatReport : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public int Year { get; set; }
        public int Quarter { get; private set; }
        public float Happiness { get; private set; }

        public int Tax { get; private set; }
        public int DestroyIncomes { get; private set; }
        public int BuildExpenses { get; private set; }
        public int MaintainanceCosts { get; private set; }

        public int Incomes { get; private set; }
        public int Expenses { get; private set; }
        public int Total { get; private set; }

        public int Population { get; private set; }
        public int PopulationChange { get; private set; }

        public int ElectricityProduced { get; private set; }
        public int ElectricityConsumed { get; private set; }

        public StatReport()
        {

        }
    }
}