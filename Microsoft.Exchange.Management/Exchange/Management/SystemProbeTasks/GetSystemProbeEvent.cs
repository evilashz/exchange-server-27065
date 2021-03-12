using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Reflection;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemProbeTasks
{
	// Token: 0x02000DA3 RID: 3491
	[Cmdlet("Get", "SystemProbeEvent")]
	public sealed class GetSystemProbeEvent : Task
	{
		// Token: 0x170029A4 RID: 10660
		// (get) Token: 0x060085D0 RID: 34256 RVA: 0x002239BA File Offset: 0x00221BBA
		// (set) Token: 0x060085D1 RID: 34257 RVA: 0x002239C2 File Offset: 0x00221BC2
		[Parameter(Position = 0, Mandatory = true, ValueFromPipeline = true)]
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
			set
			{
				this.guid = value;
			}
		}

		// Token: 0x060085D2 RID: 34258 RVA: 0x00223A40 File Offset: 0x00221C40
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				Action<Type> action = delegate(Type type)
				{
					MethodInfo method = type.GetMethod("GetProbeEvents", BindingFlags.Static | BindingFlags.Public);
					List<object> list = method.Invoke(null, new object[]
					{
						this.guid
					}) as List<object>;
					list.ForEach(delegate(object e)
					{
						base.WriteObject(e);
					});
					if (list.Count == 0)
					{
						this.WriteWarning(Strings.NoSystemProbeEventFound(this.guid));
					}
				};
				SystemProbeAssemblyHelper.Invoke(this, action);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x040040E8 RID: 16616
		private Guid guid;
	}
}
