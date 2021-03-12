using System;
using System.Globalization;
using System.Net;
using System.Xml;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync.SchemaConverter.AirSync
{
	// Token: 0x02000167 RID: 359
	[Serializable]
	internal class AirSyncStartDueDateProperty : AirSyncProperty, IStartDueDateProperty, IProperty
	{
		// Token: 0x0600102B RID: 4139 RVA: 0x0005B750 File Offset: 0x00059950
		public AirSyncStartDueDateProperty(string xmlNodeNamespace) : base(xmlNodeNamespace, new string[]
		{
			"UtcStartDate",
			"StartDate",
			"UtcDueDate",
			"DueDate"
		}, true)
		{
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x0005B78D File Offset: 0x0005998D
		public ExDateTime? DueDate
		{
			get
			{
				if (!this.datesParsed)
				{
					this.ParseDates();
				}
				return this.dates[3];
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x0005B7AE File Offset: 0x000599AE
		public ExDateTime? StartDate
		{
			get
			{
				if (!this.datesParsed)
				{
					this.ParseDates();
				}
				return this.dates[1];
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x0005B7CF File Offset: 0x000599CF
		public ExDateTime? UtcDueDate
		{
			get
			{
				if (!this.datesParsed)
				{
					this.ParseDates();
				}
				return this.dates[2];
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x0600102F RID: 4143 RVA: 0x0005B7F0 File Offset: 0x000599F0
		public ExDateTime? UtcStartDate
		{
			get
			{
				if (!this.datesParsed)
				{
					this.ParseDates();
				}
				return this.dates[0];
			}
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x0005B811 File Offset: 0x00059A11
		public override void Unbind()
		{
			base.Unbind();
			this.datesParsed = false;
			this.dates = null;
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x0005B828 File Offset: 0x00059A28
		protected override void InternalCopyFrom(IProperty srcProperty)
		{
			IStartDueDateProperty startDueDateProperty = srcProperty as IStartDueDateProperty;
			if (startDueDateProperty == null)
			{
				throw new UnexpectedTypeException("IStartDueDateProperty", srcProperty);
			}
			if (startDueDateProperty.UtcStartDate != null)
			{
				base.CreateAirSyncNode(base.AirSyncTagNames[0], startDueDateProperty.UtcStartDate.Value.ToString("yyyy-MM-dd\\THH:mm:ss.fff\\Z", DateTimeFormatInfo.InvariantInfo));
			}
			if (startDueDateProperty.StartDate != null)
			{
				base.CreateAirSyncNode(base.AirSyncTagNames[1], startDueDateProperty.StartDate.Value.ToString("yyyy-MM-dd\\THH:mm:ss.fff\\Z", DateTimeFormatInfo.InvariantInfo));
			}
			if (startDueDateProperty.UtcDueDate != null)
			{
				base.CreateAirSyncNode(base.AirSyncTagNames[2], startDueDateProperty.UtcDueDate.Value.ToString("yyyy-MM-dd\\THH:mm:ss.fff\\Z", DateTimeFormatInfo.InvariantInfo));
			}
			if (startDueDateProperty.DueDate != null)
			{
				base.CreateAirSyncNode(base.AirSyncTagNames[3], startDueDateProperty.DueDate.Value.ToString("yyyy-MM-dd\\THH:mm:ss.fff\\Z", DateTimeFormatInfo.InvariantInfo));
			}
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0005B94C File Offset: 0x00059B4C
		private void ParseDates()
		{
			this.dates = new ExDateTime?[4];
			for (int i = 0; i < this.dates.Length; i++)
			{
				XmlNode xmlNode = base.XmlNode.ParentNode[base.AirSyncTagNames[i]];
				if (xmlNode != null)
				{
					ExDateTime value;
					if (!ExDateTime.TryParseExact(xmlNode.InnerText, "yyyy-MM-dd\\THH:mm:ss.fff\\Z", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out value))
					{
						throw new AirSyncPermanentException(HttpStatusCode.BadRequest, StatusCode.InvalidDateTime, null, false)
						{
							ErrorStringForProtocolLogger = "InvalidDateTimeInAirSyncStartDueDate"
						};
					}
					this.dates[i] = new ExDateTime?(value);
				}
			}
			this.datesParsed = true;
		}

		// Token: 0x04000A82 RID: 2690
		private ExDateTime?[] dates;

		// Token: 0x04000A83 RID: 2691
		private bool datesParsed;
	}
}
