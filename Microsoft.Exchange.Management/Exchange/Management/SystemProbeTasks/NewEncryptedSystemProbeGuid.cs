using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.SystemProbeTasks
{
	// Token: 0x02000DA6 RID: 3494
	[OutputType(new Type[]
	{
		typeof(SystemProbeData)
	})]
	[Cmdlet("New", "EncryptedSystemProbeGuid")]
	public sealed class NewEncryptedSystemProbeGuid : Task
	{
		// Token: 0x170029A7 RID: 10663
		// (get) Token: 0x060085DF RID: 34271 RVA: 0x00223C80 File Offset: 0x00221E80
		// (set) Token: 0x060085E0 RID: 34272 RVA: 0x00223CB0 File Offset: 0x00221EB0
		[Parameter(Mandatory = false)]
		public Guid? Guid
		{
			get
			{
				return new Guid?(this.guid ?? System.Guid.Empty);
			}
			set
			{
				this.guid = value;
			}
		}

		// Token: 0x170029A8 RID: 10664
		// (get) Token: 0x060085E1 RID: 34273 RVA: 0x00223CBC File Offset: 0x00221EBC
		// (set) Token: 0x060085E2 RID: 34274 RVA: 0x00223CE6 File Offset: 0x00221EE6
		[Parameter(Mandatory = false)]
		public DateTime TimeStamp
		{
			get
			{
				DateTime? dateTime = this.time;
				if (dateTime == null)
				{
					return DateTime.MinValue;
				}
				return dateTime.GetValueOrDefault();
			}
			set
			{
				this.time = new DateTime?(value);
			}
		}

		// Token: 0x060085E3 RID: 34275 RVA: 0x00223CF4 File Offset: 0x00221EF4
		protected override void InternalProcessRecord()
		{
			if (this.guid == null)
			{
				this.guid = new Guid?(System.Guid.NewGuid());
			}
			if (this.time == null)
			{
				this.time = new DateTime?(DateTime.UtcNow);
			}
			string cypherText = SystemProbeId.EncryptProbeGuid(this.guid.Value, this.time.Value);
			base.WriteObject(this.CreateSystemProbeData(this.guid.Value, this.time.Value, cypherText));
		}

		// Token: 0x060085E4 RID: 34276 RVA: 0x00223D7C File Offset: 0x00221F7C
		private SystemProbeData CreateSystemProbeData(Guid guid, DateTime timeStamp, string cypherText)
		{
			return new SystemProbeData
			{
				Guid = guid,
				TimeStamp = timeStamp,
				Text = cypherText
			};
		}

		// Token: 0x040040EB RID: 16619
		private Guid? guid;

		// Token: 0x040040EC RID: 16620
		private DateTime? time;
	}
}
