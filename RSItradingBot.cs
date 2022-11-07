//Declaring ninja trader packages
#region Using declarations
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.SuperDom;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript.Indicators;
using NinjaTrader.NinjaScript.DrawingTools;
#endregion

namespace NinjaTrader.NinjaScript.Strategies
{
	public class RSIStrategy : Strategy
	{
		protected override void OnStateChange()
		{
			if (State == State.SetDefaults)
			{
				//Strategy Defaults
				Name										= "RSIStrategy";
				Calculate									= Calculate.OnBarClose;
				EntriesPerDirection							= 1;
				EntryHandling								= EntryHandling.AllEntries;
				IsExitOnSessionCloseStrategy				= true;
				ExitOnSessionCloseSeconds					= 30;
				IsFillLimitOnTouch							= false;
				MaximumBarsLookBack							= MaximumBarsLookBack.TwoHundredFiftySix;
				OrderFillResolution							= OrderFillResolution.Standard;
				Slippage									= 0;
				StartBehavior								= StartBehavior.WaitUntilFlat;
				TimeInForce									= TimeInForce.Gtc;
				TraceOrders									= false;
				RealtimeErrorHandling						= RealtimeErrorHandling.StopCancelClose;
				StopTargetHandling							= StopTargetHandling.PerEntryExecution;
				BarsRequiredToTrade							= 20;
			}
			else if (State == State.Configure)
			{
				AddChartIndicator(RSI(13, 3));
				//Setting Parabolic Stop Loss for 1.5%
				SetParabolicStop(CalculationMode.Percent, 0.015);
			}
		}

		protected override void OnBarUpdate()
		{
			base.OnBarUpdate();

			if (CurrentBars[0] < BarsRequiredToTrade)
				return;
			//Creating condition for buy
			if (CrossAbove(RSI(13, 3).Values[1], 7, 1))
				EnterLong();
		}
	}
}
