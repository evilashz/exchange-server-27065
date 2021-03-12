using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000B05 RID: 2821
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SettingOverrideInvalidFlightNameException : SettingOverrideException
	{
		// Token: 0x060081C3 RID: 33219 RVA: 0x001A7001 File Offset: 0x001A5201
		public SettingOverrideInvalidFlightNameException(string flightName, string availableFlights) : base(DirectoryStrings.ErrorSettingOverrideInvalidFlightName(flightName, availableFlights))
		{
			this.flightName = flightName;
			this.availableFlights = availableFlights;
		}

		// Token: 0x060081C4 RID: 33220 RVA: 0x001A701E File Offset: 0x001A521E
		public SettingOverrideInvalidFlightNameException(string flightName, string availableFlights, Exception innerException) : base(DirectoryStrings.ErrorSettingOverrideInvalidFlightName(flightName, availableFlights), innerException)
		{
			this.flightName = flightName;
			this.availableFlights = availableFlights;
		}

		// Token: 0x060081C5 RID: 33221 RVA: 0x001A703C File Offset: 0x001A523C
		protected SettingOverrideInvalidFlightNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.flightName = (string)info.GetValue("flightName", typeof(string));
			this.availableFlights = (string)info.GetValue("availableFlights", typeof(string));
		}

		// Token: 0x060081C6 RID: 33222 RVA: 0x001A7091 File Offset: 0x001A5291
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("flightName", this.flightName);
			info.AddValue("availableFlights", this.availableFlights);
		}

		// Token: 0x17002F06 RID: 12038
		// (get) Token: 0x060081C7 RID: 33223 RVA: 0x001A70BD File Offset: 0x001A52BD
		public string FlightName
		{
			get
			{
				return this.flightName;
			}
		}

		// Token: 0x17002F07 RID: 12039
		// (get) Token: 0x060081C8 RID: 33224 RVA: 0x001A70C5 File Offset: 0x001A52C5
		public string AvailableFlights
		{
			get
			{
				return this.availableFlights;
			}
		}

		// Token: 0x040055E0 RID: 21984
		private readonly string flightName;

		// Token: 0x040055E1 RID: 21985
		private readonly string availableFlights;
	}
}
