using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Reflection;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemProbeTasks
{
	// Token: 0x02000DA5 RID: 3493
	[OutputType(new Type[]
	{
		typeof(Guid)
	})]
	[Cmdlet("Get", "SystemProbe")]
	public sealed class GetSystemProbe : Task
	{
		// Token: 0x170029A5 RID: 10661
		// (get) Token: 0x060085D7 RID: 34263 RVA: 0x00223B34 File Offset: 0x00221D34
		// (set) Token: 0x060085D8 RID: 34264 RVA: 0x00223B3C File Offset: 0x00221D3C
		[Parameter(Mandatory = false)]
		public DateTimeOffset? StartTime
		{
			get
			{
				return this.startTime;
			}
			set
			{
				this.startTime = value;
			}
		}

		// Token: 0x170029A6 RID: 10662
		// (get) Token: 0x060085D9 RID: 34265 RVA: 0x00223B45 File Offset: 0x00221D45
		// (set) Token: 0x060085DA RID: 34266 RVA: 0x00223B4D File Offset: 0x00221D4D
		[Parameter(Mandatory = false)]
		public DateTimeOffset? EndTime
		{
			get
			{
				return this.endTime;
			}
			set
			{
				this.endTime = value;
			}
		}

		// Token: 0x060085DB RID: 34267 RVA: 0x00223C30 File Offset: 0x00221E30
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				Action<Type> action = delegate(Type type)
				{
					DateTimeOffset dateTimeOffset = this.StartTime ?? (DateTimeOffset.UtcNow - TimeSpan.FromHours(24.0));
					DateTimeOffset dateTimeOffset2 = this.EndTime ?? DateTimeOffset.UtcNow;
					MethodInfo method = type.GetMethod("GetProbes", BindingFlags.Static | BindingFlags.Public);
					List<Guid> list = method.Invoke(null, new object[]
					{
						dateTimeOffset,
						dateTimeOffset2
					}) as List<Guid>;
					list.ForEach(delegate(Guid g)
					{
						base.WriteObject(g);
					});
					if (list.Count == 0)
					{
						this.WriteWarning(Strings.NoSystemProbesFound(dateTimeOffset.DateTime, dateTimeOffset2.DateTime));
					}
				};
				SystemProbeAssemblyHelper.Invoke(this, action);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x040040E9 RID: 16617
		private DateTimeOffset? startTime;

		// Token: 0x040040EA RID: 16618
		private DateTimeOffset? endTime;
	}
}
