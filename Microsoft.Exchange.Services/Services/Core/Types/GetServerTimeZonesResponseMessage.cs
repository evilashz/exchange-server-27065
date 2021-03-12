using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000513 RID: 1299
	[KnownType(typeof(TimeZoneDefinitionType))]
	[KnownType(typeof(ArrayOfTransitionsType))]
	[KnownType(typeof(TimeZoneDefinitionType))]
	[KnownType(typeof(ResponseMessage))]
	[XmlType("GetServerTimeZonesResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(PeriodType))]
	[KnownType(typeof(GetServerTimeZonesResponseMessage))]
	public class GetServerTimeZonesResponseMessage : ResponseMessage
	{
		// Token: 0x0600255B RID: 9563 RVA: 0x000A59A7 File Offset: 0x000A3BA7
		public GetServerTimeZonesResponseMessage()
		{
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x000A59AF File Offset: 0x000A3BAF
		internal GetServerTimeZonesResponseMessage(ServiceResultCode code, ServiceError error, GetServerTimeZoneResultType timeZones) : base(code, error)
		{
			this.TimeZoneResultType = timeZones;
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x0600255D RID: 9565 RVA: 0x000A59C0 File Offset: 0x000A3BC0
		// (set) Token: 0x0600255E RID: 9566 RVA: 0x000A59C8 File Offset: 0x000A3BC8
		[IgnoreDataMember]
		[XmlIgnore]
		internal GetServerTimeZoneResultType TimeZoneResultType { get; set; }

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x0600255F RID: 9567 RVA: 0x000A59D1 File Offset: 0x000A3BD1
		// (set) Token: 0x06002560 RID: 9568 RVA: 0x000A59F2 File Offset: 0x000A3BF2
		[XmlArrayItem("TimeZoneDefinition", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[XmlArray("TimeZoneDefinitions", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "TimeZoneDefinitions", EmitDefaultValue = false)]
		public TimeZoneDefinitionType[] TimeZoneDefinitions
		{
			get
			{
				if (this.timeZoneDefinitions == null)
				{
					this.timeZoneDefinitions = this.TimeZoneResultType.ToTimeZoneDefinitionType();
				}
				return this.timeZoneDefinitions;
			}
			set
			{
				this.timeZoneDefinitions = value;
			}
		}

		// Token: 0x040015B9 RID: 5561
		private TimeZoneDefinitionType[] timeZoneDefinitions;
	}
}
