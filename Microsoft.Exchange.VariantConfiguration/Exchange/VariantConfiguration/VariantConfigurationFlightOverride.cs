using System;

namespace Microsoft.Exchange.VariantConfiguration
{
	// Token: 0x02000143 RID: 323
	public sealed class VariantConfigurationFlightOverride : VariantConfigurationOverride
	{
		// Token: 0x06000EFA RID: 3834 RVA: 0x000252F2 File Offset: 0x000234F2
		public VariantConfigurationFlightOverride(string flightName, params string[] parameters) : base(flightName, flightName, parameters)
		{
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06000EFB RID: 3835 RVA: 0x000252FD File Offset: 0x000234FD
		public override string FileName
		{
			get
			{
				if (VariantConfiguration.Flights.Contains(this.FlightName))
				{
					return VariantConfiguration.Flights[this.FlightName].FileName;
				}
				return this.FlightName + ".flight.ini";
			}
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x00025337 File Offset: 0x00023537
		public string FlightName
		{
			get
			{
				return base.SectionName;
			}
		}
	}
}
