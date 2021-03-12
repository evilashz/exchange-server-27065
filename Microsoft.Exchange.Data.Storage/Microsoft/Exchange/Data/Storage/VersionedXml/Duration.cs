using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Storage.VersionedXml
{
	// Token: 0x02000ECB RID: 3787
	[Serializable]
	public class Duration
	{
		// Token: 0x0600829B RID: 33435 RVA: 0x00239D63 File Offset: 0x00237F63
		public Duration()
		{
		}

		// Token: 0x0600829C RID: 33436 RVA: 0x00239D6B File Offset: 0x00237F6B
		public Duration(DurationType type, uint interval, bool useWorkHoursTimeSlot, DateTime startTimeInDay, DateTime endTimeInDay, bool nonWorkHoursExcluded)
		{
			this.Type = type;
			this.Interval = interval;
			this.UseWorkHoursTimeSlot = useWorkHoursTimeSlot;
			this.StartTimeInDay = startTimeInDay;
			this.EndTimeInDay = endTimeInDay;
			this.NonWorkHoursExcluded = nonWorkHoursExcluded;
		}

		// Token: 0x1700229A RID: 8858
		// (get) Token: 0x0600829D RID: 33437 RVA: 0x00239DA0 File Offset: 0x00237FA0
		// (set) Token: 0x0600829E RID: 33438 RVA: 0x00239DA8 File Offset: 0x00237FA8
		[XmlElement("Type")]
		public DurationType Type { get; set; }

		// Token: 0x1700229B RID: 8859
		// (get) Token: 0x0600829F RID: 33439 RVA: 0x00239DB1 File Offset: 0x00237FB1
		// (set) Token: 0x060082A0 RID: 33440 RVA: 0x00239DB9 File Offset: 0x00237FB9
		[XmlElement("Interval")]
		public uint Interval { get; set; }

		// Token: 0x1700229C RID: 8860
		// (get) Token: 0x060082A1 RID: 33441 RVA: 0x00239DC2 File Offset: 0x00237FC2
		// (set) Token: 0x060082A2 RID: 33442 RVA: 0x00239DCA File Offset: 0x00237FCA
		[XmlElement("UseWorkHoursTimeSlot")]
		public bool UseWorkHoursTimeSlot { get; set; }

		// Token: 0x1700229D RID: 8861
		// (get) Token: 0x060082A3 RID: 33443 RVA: 0x00239DD3 File Offset: 0x00237FD3
		// (set) Token: 0x060082A4 RID: 33444 RVA: 0x00239DDB File Offset: 0x00237FDB
		[XmlElement("StartTimeInDay")]
		public DateTime StartTimeInDay { get; set; }

		// Token: 0x1700229E RID: 8862
		// (get) Token: 0x060082A5 RID: 33445 RVA: 0x00239DE4 File Offset: 0x00237FE4
		// (set) Token: 0x060082A6 RID: 33446 RVA: 0x00239DEC File Offset: 0x00237FEC
		[XmlElement("EndTimeInDay")]
		public DateTime EndTimeInDay { get; set; }

		// Token: 0x1700229F RID: 8863
		// (get) Token: 0x060082A7 RID: 33447 RVA: 0x00239DF5 File Offset: 0x00237FF5
		// (set) Token: 0x060082A8 RID: 33448 RVA: 0x00239DFD File Offset: 0x00237FFD
		[XmlElement("NonWorkHoursExcluded")]
		public bool NonWorkHoursExcluded { get; set; }
	}
}
