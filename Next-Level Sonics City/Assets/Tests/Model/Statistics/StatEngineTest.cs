﻿using Model.Persons;
using Model.Tiles;
using Model.Tiles.Buildings;
using NUnit.Framework;
using System.Collections.Generic;

namespace Model.Statistics
{
	public class StatEngineTests
	{
		RoadTile _road;
		ResidentialBuildingTile _residential1;
		ResidentialBuildingTile _residential2;
		CommercialBuildingTIle _workplace1;
		CommercialBuildingTIle _workplace2;

		List<Worker> _workers;
		List<Pensioner> _pensioners;

		[SetUp]
		public void SetUp()
		{
			StatEngine.Reset();
			City.Reset();
			for (int i = 0; i < City.Instance.GetSize(); i++)
			{
				for (int j = 0; j < City.Instance.GetSize(); j++)
				{
					City.Instance.SetTile(new EmptyTile(i, j));
				}
			}

			_road = new(1, 1);
			City.Instance.SetTile(_road);
			_residential1 = new(1, 0, 0, Rotation.OneEighty, ZoneBuildingLevel.ZERO);
			City.Instance.SetTile(_residential1);
			_residential2 = new(2, 1, 0, Rotation.TwoSeventy, ZoneBuildingLevel.ZERO);
			City.Instance.SetTile(_residential2);

			_workplace1 = new(1, 2, 0, Rotation.Zero, ZoneBuildingLevel.ZERO);
			City.Instance.SetTile(_workplace1);
			_workplace2 = new(0, 1, 0, Rotation.Ninety, ZoneBuildingLevel.ZERO);
			City.Instance.SetTile(_workplace2);

			_workers = new()
			{
				new Worker(_residential1, _workplace1, 30, Qualification.LOW),
				new Worker(_residential1, _workplace2, 40, Qualification.LOW),
				new Worker(_residential2, _workplace1, 50, Qualification.LOW),
				new Worker(_residential2, _workplace2, 60, Qualification.LOW)
			};

			_pensioners = new()
			{
				new Pensioner(_residential1, 70, 25),
				new Pensioner(_residential1, 80, 50),
				new Pensioner(_residential2, 90, 75),
				new Pensioner(_residential2, 100, 100)
			};
		}

		[Test]
		public void CalculateResidentialTaxPerResidential_EqualsToIndivideualWorkersTaxSum()
		{
			float taxRate = 0.1f;
			float residentialTax = _workers[0].PayTax(taxRate) + _workers[1].PayTax(taxRate);
			
			Assert.AreEqual(residentialTax, StatEngine.Instance.CalculateResidentialTaxPerResidential(_residential1, taxRate));
		}

		[Test]
		public void CalculateResidentialTax_EqualsToIndividualWorkersTaxSum()
		{
			float taxRate = 0.1f;
			float residentialTax = 0;
			foreach (Worker worker in _workers)
			{
				residentialTax += worker.PayTax(taxRate);
			}

			Assert.AreEqual(residentialTax, StatEngine.Instance.CalculateResidentialTax(new List<IResidential>() { _residential1, _residential2 }, taxRate));
		}

		[Test]
		public void CalculateWorkplaceTaxPerWorkplace_EqualsToIndivideualWorkersTaxSum()
		{
			float taxRate = 0.1f;
			float workplaceTax = _workers[0].PayTax(taxRate) + _workers[2].PayTax(taxRate);
			Assert.AreEqual(workplaceTax, StatEngine.Instance.CalculateWorkplaceTaxPerWorkplace(_workplace1, taxRate));
		}

		[Test]
		public void CalculateWorkplaceTax_EqualsToIndividualWorkersTaxSum()
		{
			float taxRate = 0.1f;
			float workplaceTax = 0;
			foreach (Worker worker in _workers)
			{
				workplaceTax += worker.PayTax(taxRate);
			}
			Assert.AreEqual(workplaceTax, StatEngine.Instance.CalculateWorkplaceTax(new List<IWorkplace>() { _workplace1, _workplace2 }, taxRate));
		}

		[Test]
		public void CalculatePensionPerResidential_EqualsToIndividualPensionersPensionSum()
		{
			float pension = _pensioners[0].Pension + _pensioners[1].Pension;
			Assert.AreEqual(pension, StatEngine.Instance.CalculatePensionPerResidential(_residential1));
		}

		[Test]
		public void CalculatePension_EqualsToIndividualPensionersPensionSum()
		{
			float pension = 0;
			foreach (Pensioner pensioner in _pensioners)
			{
				pension += pensioner.Pension;
			}
			Assert.AreEqual(pension, StatEngine.Instance.CalculatePension(new List<IResidential>() { _residential1, _residential2 }));
		}

		[Test]
		public void SumMaintenance_EqualsToIndividualTilesMaintenanceSum()
		{
			float maintenance = _residential1.MaintainanceCost + _residential2.MaintainanceCost + _workplace1.MaintainanceCost + _workplace2.MaintainanceCost;
			Assert.AreEqual(maintenance, StatEngine.Instance.SumMaintenance(new List<Tile>() { _residential1, _residential2, _workplace1, _workplace2 }));
		}
	}
}