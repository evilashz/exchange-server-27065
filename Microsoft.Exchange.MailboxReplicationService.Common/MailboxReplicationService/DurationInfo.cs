using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000048 RID: 72
	[DataContract]
	[Serializable]
	public class DurationInfo
	{
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000367 RID: 871 RVA: 0x00006049 File Offset: 0x00004249
		// (set) Token: 0x06000368 RID: 872 RVA: 0x00006051 File Offset: 0x00004251
		[XmlIgnore]
		[IgnoreDataMember]
		public TimeSpan Duration { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000605A File Offset: 0x0000425A
		// (set) Token: 0x0600036A RID: 874 RVA: 0x00006062 File Offset: 0x00004262
		[DataMember(Name = "Name")]
		[XmlElement(ElementName = "Name")]
		public string Name { get; set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000606C File Offset: 0x0000426C
		// (set) Token: 0x0600036C RID: 876 RVA: 0x00006087 File Offset: 0x00004287
		[DataMember(Name = "DurationTicks", IsRequired = false)]
		[XmlElement(ElementName = "DurationTicks")]
		public long DurationTicks
		{
			get
			{
				return this.Duration.Ticks;
			}
			set
			{
				this.Duration = new TimeSpan(value);
			}
		}
	}
}
