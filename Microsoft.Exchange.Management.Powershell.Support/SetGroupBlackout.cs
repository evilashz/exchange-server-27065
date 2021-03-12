using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.MailboxReplicationService.Upgrade14to15;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x02000042 RID: 66
	[Cmdlet("Set", "GroupBlackout", DefaultParameterSetName = "SingleGroupBlackoutUpdate")]
	public class SetGroupBlackout : SymphonyTaskBase
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000E62C File Offset: 0x0000C82C
		// (set) Token: 0x0600032A RID: 810 RVA: 0x0000E634 File Offset: 0x0000C834
		[Parameter(Mandatory = false, ParameterSetName = "SingleGroupBlackoutUpdate")]
		public DateTime StartDate { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000E63D File Offset: 0x0000C83D
		// (set) Token: 0x0600032C RID: 812 RVA: 0x0000E645 File Offset: 0x0000C845
		[Parameter(Mandatory = false, ParameterSetName = "SingleGroupBlackoutUpdate")]
		public DateTime EndDate { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000E64E File Offset: 0x0000C84E
		// (set) Token: 0x0600032E RID: 814 RVA: 0x0000E656 File Offset: 0x0000C856
		[Parameter(Mandatory = false, ParameterSetName = "SingleGroupBlackoutUpdate")]
		public string Reason { get; set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000E65F File Offset: 0x0000C85F
		// (set) Token: 0x06000330 RID: 816 RVA: 0x0000E667 File Offset: 0x0000C867
		[Parameter(Mandatory = false, ParameterSetName = "SingleGroupBlackoutUpdate")]
		public BlackoutInterval[] BlackoutIntervals { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000E670 File Offset: 0x0000C870
		// (set) Token: 0x06000332 RID: 818 RVA: 0x0000E678 File Offset: 0x0000C878
		[Parameter(Mandatory = true, ParameterSetName = "SingleGroupBlackoutUpdate")]
		public string Group { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000E681 File Offset: 0x0000C881
		// (set) Token: 0x06000334 RID: 820 RVA: 0x0000E689 File Offset: 0x0000C889
		[Parameter(Mandatory = true, ParameterSetName = "MultiGroupBlackoutUpdate")]
		public PSObject[] GroupBlackouts { get; set; }

		// Token: 0x06000335 RID: 821 RVA: 0x0000E6C0 File Offset: 0x0000C8C0
		protected override void InternalProcessRecord()
		{
			SetGroupBlackout.<>c__DisplayClass1 CS$<>8__locals1 = new SetGroupBlackout.<>c__DisplayClass1();
			CS$<>8__locals1.toUpdate = null;
			string parameterSetName;
			if ((parameterSetName = base.ParameterSetName) != null)
			{
				if (!(parameterSetName == "SingleGroupBlackoutUpdate"))
				{
					if (parameterSetName == "MultiGroupBlackoutUpdate")
					{
						Dictionary<string, List<BlackoutInterval>> dictionary = new Dictionary<string, List<BlackoutInterval>>();
						foreach (PSObject psobject in this.GroupBlackouts)
						{
							string key = base.GetPropertyValue(psobject.Properties, "GroupName").ToString();
							string reason = base.GetPropertyValue(psobject.Properties, "Reason").ToString();
							DateTime startDate;
							DateTime.TryParse(base.GetPropertyValue(psobject.Properties, "StartDate").ToString(), out startDate);
							DateTime endDate;
							DateTime.TryParse(base.GetPropertyValue(psobject.Properties, "EndDate").ToString(), out endDate);
							if (dictionary.ContainsKey(key))
							{
								if (dictionary[key].Count >= 20)
								{
									base.ThrowTerminatingError(new PSArgumentException("Cannot update more than 20 BlackoutIntervals per group"), ErrorCategory.InvalidArgument, this.GroupBlackouts);
								}
								dictionary[key].Add(new BlackoutInterval(startDate, endDate, reason));
							}
							else
							{
								dictionary.Add(key, new List<BlackoutInterval>
								{
									new BlackoutInterval(startDate, endDate, reason)
								});
							}
						}
						List<GroupBlackout> list = new List<GroupBlackout>();
						foreach (KeyValuePair<string, List<BlackoutInterval>> keyValuePair in dictionary)
						{
							list.Add(new GroupBlackout(keyValuePair.Key, keyValuePair.Value.ToArray()));
						}
						CS$<>8__locals1.toUpdate = list.ToArray();
					}
				}
				else
				{
					BlackoutInterval[] intervals;
					if (this.BlackoutIntervals == null)
					{
						BlackoutInterval blackoutInterval = new BlackoutInterval(this.StartDate, this.EndDate, this.Reason);
						intervals = new BlackoutInterval[]
						{
							blackoutInterval
						};
					}
					else
					{
						intervals = this.BlackoutIntervals;
					}
					GroupBlackout groupBlackout = new GroupBlackout(this.Group, intervals);
					CS$<>8__locals1.toUpdate = new GroupBlackout[]
					{
						groupBlackout
					};
				}
			}
			using (ProxyWrapper<UpgradeSchedulingConstraintsClient, IUpgradeSchedulingConstraints> workloadClient = new ProxyWrapper<UpgradeSchedulingConstraintsClient, IUpgradeSchedulingConstraints>(base.WorkloadUri, base.Certificate))
			{
				workloadClient.CallSymphony(delegate
				{
					workloadClient.Proxy.UpdateBlackout(CS$<>8__locals1.toUpdate);
				}, base.WorkloadUri.ToString());
			}
		}

		// Token: 0x04000124 RID: 292
		private const string SingleGroupBlackoutUpdate = "SingleGroupBlackoutUpdate";

		// Token: 0x04000125 RID: 293
		private const string MultiGroupBlackoutUpdate = "MultiGroupBlackoutUpdate";
	}
}
