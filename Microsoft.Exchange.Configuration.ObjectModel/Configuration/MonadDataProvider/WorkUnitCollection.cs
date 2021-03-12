using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001E4 RID: 484
	internal class WorkUnitCollection : BindingList<WorkUnit>
	{
		// Token: 0x0600116F RID: 4463 RVA: 0x0003554D File Offset: 0x0003374D
		public WorkUnitCollection()
		{
			this.stopWatch = new Stopwatch();
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x00035560 File Offset: 0x00033760
		public void AddRange(IEnumerable<WorkUnit> workUnits)
		{
			base.RaiseListChangedEvents = false;
			foreach (WorkUnit item in workUnits)
			{
				base.Add(item);
			}
			base.RaiseListChangedEvents = true;
			base.ResetBindings();
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x000355BC File Offset: 0x000337BC
		protected override void OnListChanged(ListChangedEventArgs e)
		{
			if (e.ListChangedType == ListChangedType.ItemChanged && e.PropertyDescriptor != null && e.PropertyDescriptor.Name == "Status")
			{
				WorkUnit workUnit = base[e.NewIndex];
				if (!this.stopWatch.IsRunning && workUnit.Status == WorkUnitStatus.InProgress)
				{
					this.stopWatch.Start();
				}
				else
				{
					bool flag = false;
					bool flag2 = true;
					for (int i = 0; i < base.Count; i++)
					{
						if (base[i].Status == WorkUnitStatus.InProgress)
						{
							flag = true;
						}
						if (base[i].Status != WorkUnitStatus.NotStarted)
						{
							flag2 = false;
						}
					}
					if (flag2)
					{
						this.stopWatch.Reset();
					}
					if (flag)
					{
						if (!this.stopWatch.IsRunning)
						{
							this.stopWatch.Start();
						}
					}
					else
					{
						this.stopWatch.Stop();
					}
				}
			}
			base.OnListChanged(e);
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x000356A0 File Offset: 0x000338A0
		public int ProgressValue
		{
			get
			{
				int num = 0;
				checked
				{
					for (int i = 0; i < base.Count; i++)
					{
						num += base[i].PercentComplete;
					}
					return num;
				}
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06001173 RID: 4467 RVA: 0x000356D0 File Offset: 0x000338D0
		public int MaxProgressValue
		{
			get
			{
				return checked(Math.Max(base.Count, 1) * 100);
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x000356E4 File Offset: 0x000338E4
		public LocalizedString Description
		{
			get
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				for (int i = 0; i < base.Count; i++)
				{
					switch (base[i].Status)
					{
					case WorkUnitStatus.NotStarted:
						num++;
						break;
					case WorkUnitStatus.InProgress:
						num4++;
						break;
					case WorkUnitStatus.Completed:
						num2++;
						break;
					case WorkUnitStatus.Failed:
						num3++;
						break;
					}
				}
				if (num == base.Count)
				{
					return Strings.WorkUnitCollectionConfigurationSummary;
				}
				return Strings.WorkUnitCollectionStatus(base.Count, num2, num3);
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06001175 RID: 4469 RVA: 0x00035768 File Offset: 0x00033968
		public bool AllCompleted
		{
			get
			{
				if (base.Count == 0)
				{
					return false;
				}
				for (int i = 0; i < base.Count; i++)
				{
					if (base[i].Status != WorkUnitStatus.Completed)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x000357A4 File Offset: 0x000339A4
		public bool HasFailures
		{
			get
			{
				for (int i = 0; i < base.Count; i++)
				{
					if (base[i].Status == WorkUnitStatus.Failed || base[i].Status == WorkUnitStatus.NotStarted)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06001177 RID: 4471 RVA: 0x000357E4 File Offset: 0x000339E4
		public bool Cancelled
		{
			get
			{
				foreach (WorkUnit workUnit in this)
				{
					if (workUnit.Status == WorkUnitStatus.Failed && workUnit.Errors.Count > 0 && workUnit.Errors[0].Exception is PipelineStoppedException)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x0003585C File Offset: 0x00033A5C
		public bool IsDataChanged
		{
			get
			{
				return this.FindByStatus(WorkUnitStatus.Completed).Count > 0;
			}
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00035870 File Offset: 0x00033A70
		public IList<WorkUnit> FindByStatus(WorkUnitStatus status)
		{
			List<WorkUnit> list = new List<WorkUnit>();
			for (int i = 0; i < base.Count; i++)
			{
				if (status == base[i].Status)
				{
					list.Add(base[i]);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x000358B8 File Offset: 0x00033AB8
		public IList<WorkUnit> FindByErrorOrWarning()
		{
			List<WorkUnit> list = new List<WorkUnit>();
			for (int i = 0; i < base.Count; i++)
			{
				if (base[i].Errors.Count > 0 || base[i].Warnings.Count > 0)
				{
					list.Add(base[i]);
				}
			}
			return list.ToArray();
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x0600117B RID: 4475 RVA: 0x00035918 File Offset: 0x00033B18
		public string Summary
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine(this.ElapsedTimeText);
				stringBuilder.AppendLine();
				stringBuilder.AppendLine(this.Description);
				for (int i = 0; i < base.Count; i++)
				{
					WorkUnit workUnit = base[i];
					stringBuilder.AppendLine();
					stringBuilder.AppendLine(workUnit.Summary);
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x00035984 File Offset: 0x00033B84
		public TimeSpan ElapsedTime
		{
			get
			{
				return this.stopWatch.Elapsed;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x0600117D RID: 4477 RVA: 0x00035994 File Offset: 0x00033B94
		public string ElapsedTimeText
		{
			get
			{
				TimeSpan elapsedTime = this.ElapsedTime;
				return Strings.OverallElapsedTimeDescription(elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds);
			}
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x000359C8 File Offset: 0x00033BC8
		public WorkUnit[] ToArray()
		{
			WorkUnit[] array = new WorkUnit[base.Count];
			base.CopyTo(array, 0);
			return array;
		}

		// Token: 0x040003E7 RID: 999
		private Stopwatch stopWatch;
	}
}
