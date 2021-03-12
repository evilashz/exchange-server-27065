using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001E3 RID: 483
	public sealed class WorkUnit : WorkUnitBase, INotifyPropertyChanged
	{
		// Token: 0x0600114B RID: 4427 RVA: 0x00034EA6 File Offset: 0x000330A6
		public WorkUnit()
		{
			this.description = "";
			this.text = "";
			this.canShowElapsedTime = true;
			this.canShowExecutedCommand = true;
			this.stopWatch = new Stopwatch();
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00034EDD File Offset: 0x000330DD
		public WorkUnit(string text, Icon icon) : this()
		{
			this.Text = text;
			this.Icon = icon;
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x00034EF3 File Offset: 0x000330F3
		public WorkUnit(string text, Icon icon, object target) : this(text, icon)
		{
			this.Target = target;
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x0600114E RID: 4430 RVA: 0x00034F04 File Offset: 0x00033104
		// (set) Token: 0x0600114F RID: 4431 RVA: 0x00034F0C File Offset: 0x0003310C
		public bool CanShowElapsedTime
		{
			get
			{
				return this.canShowElapsedTime;
			}
			set
			{
				if (this.CanShowElapsedTime != value)
				{
					this.canShowElapsedTime = value;
					this.RaisePropertyChanged("CanShowElapsedTime");
				}
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x00034F29 File Offset: 0x00033129
		public TimeSpan ElapsedTime
		{
			get
			{
				return this.stopWatch.Elapsed;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06001151 RID: 4433 RVA: 0x00034F38 File Offset: 0x00033138
		public string ElapsedTimeText
		{
			get
			{
				TimeSpan elapsedTime = this.ElapsedTime;
				return Strings.ElapsedTimeDescription(elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds);
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x00034F6B File Offset: 0x0003316B
		// (set) Token: 0x06001153 RID: 4435 RVA: 0x00034F73 File Offset: 0x00033173
		[DefaultValue("")]
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				value = (value ?? "");
				if (this.Description != value)
				{
					this.description = value;
					this.RaisePropertyChanged("Description");
				}
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x00034FA1 File Offset: 0x000331A1
		// (set) Token: 0x06001155 RID: 4437 RVA: 0x00034FA9 File Offset: 0x000331A9
		[DefaultValue("")]
		public string StatusDescription
		{
			get
			{
				return this.statusDescription;
			}
			set
			{
				value = (value ?? "");
				if (this.StatusDescription != value)
				{
					this.statusDescription = value;
					this.RaisePropertyChanged("StatusDescription");
				}
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x00034FD7 File Offset: 0x000331D7
		// (set) Token: 0x06001157 RID: 4439 RVA: 0x00034FDF File Offset: 0x000331DF
		public bool CanShowExecutedCommand
		{
			get
			{
				return this.canShowExecutedCommand;
			}
			set
			{
				if (this.CanShowExecutedCommand != value)
				{
					this.canShowExecutedCommand = value;
					this.RaisePropertyChanged("CanShowExecutedCommand");
				}
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x00034FFC File Offset: 0x000331FC
		// (set) Token: 0x06001159 RID: 4441 RVA: 0x00035004 File Offset: 0x00033204
		[DefaultValue("")]
		public string ExecutedCommandText
		{
			get
			{
				return this.executedCommandText;
			}
			set
			{
				if (this.ExecutedCommandText != value)
				{
					this.executedCommandText = value;
					this.RaisePropertyChanged("ExecutedCommandText");
				}
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x0600115A RID: 4442 RVA: 0x00035026 File Offset: 0x00033226
		public string ExecutedCommandTextForWorkUnit
		{
			get
			{
				if (!string.IsNullOrEmpty(this.executedCommandText) && this.Target != null)
				{
					return string.Format("{0} | {1}", MonadCommand.FormatParameterValue(this.Target), this.executedCommandText);
				}
				return this.executedCommandText;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x0600115B RID: 4443 RVA: 0x00035060 File Offset: 0x00033260
		public string ErrorsDescription
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < base.Errors.Count; i++)
				{
					ErrorRecord errorRecord = base.Errors[i];
					stringBuilder.AppendLine(Strings.WorkUnitError);
					stringBuilder.AppendLine(errorRecord.ToString());
					ErrorDetails errorDetails = errorRecord.ErrorDetails;
					if (errorDetails != null && !string.IsNullOrEmpty(errorDetails.RecommendedAction))
					{
						stringBuilder.AppendLine(string.Format("{0} {1}", Strings.HelpUrlHeaderText, errorDetails.RecommendedAction));
					}
					if (i < base.Errors.Count - 1)
					{
						stringBuilder.AppendLine();
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x0003510C File Offset: 0x0003330C
		public string WarningsDescription
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < base.Warnings.Count; i++)
				{
					stringBuilder.AppendLine(Strings.WorkUnitWarning);
					using (WarningReportEventArgs warningReportEventArgs = new WarningReportEventArgs(default(Guid), base.Warnings[i], 0, new MonadCommand()))
					{
						stringBuilder.AppendLine(warningReportEventArgs.WarningMessage);
						if (!string.IsNullOrEmpty(warningReportEventArgs.HelpUrl))
						{
							stringBuilder.AppendLine(string.Format("{0} {1}", Strings.HelpUrlHeaderText, warningReportEventArgs.HelpUrl));
						}
					}
					if (i < base.Warnings.Count - 1)
					{
						stringBuilder.AppendLine();
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x000351E4 File Offset: 0x000333E4
		// (set) Token: 0x0600115E RID: 4446 RVA: 0x000351EC File Offset: 0x000333EC
		[DefaultValue(null)]
		public Icon Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				if (this.Icon != value)
				{
					this.icon = value;
					this.RaisePropertyChanged("Icon");
				}
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x00035209 File Offset: 0x00033409
		// (set) Token: 0x06001160 RID: 4448 RVA: 0x00035211 File Offset: 0x00033411
		[DefaultValue(0)]
		public override int PercentComplete
		{
			get
			{
				return this.percentComplete;
			}
			set
			{
				value = Math.Max(0, Math.Min(value, 100));
				if (this.PercentComplete != value)
				{
					this.percentComplete = value;
					this.RaisePropertyChanged("PercentComplete");
				}
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x0003523E File Offset: 0x0003343E
		// (set) Token: 0x06001162 RID: 4450 RVA: 0x00035246 File Offset: 0x00033446
		[DefaultValue(0)]
		public override WorkUnitBaseStatus CurrentStatus
		{
			get
			{
				return base.CurrentStatus;
			}
			set
			{
				base.CurrentStatus = value;
				this.Status = this.ConvertWorkUnitBaseStatus(base.CurrentStatus);
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06001163 RID: 4451 RVA: 0x00035261 File Offset: 0x00033461
		// (set) Token: 0x06001164 RID: 4452 RVA: 0x0003526C File Offset: 0x0003346C
		[DefaultValue(WorkUnitStatus.NotStarted)]
		public WorkUnitStatus Status
		{
			get
			{
				return this.status;
			}
			set
			{
				if (this.status != value)
				{
					if (!Enum.IsDefined(typeof(WorkUnitStatus), value))
					{
						throw new InvalidEnumArgumentException("Status", (int)value, typeof(WorkUnitStatus));
					}
					this.status = value;
					this.RaisePropertyChanged("Status");
					if (this.status == WorkUnitStatus.InProgress)
					{
						this.stopWatch.Reset();
						this.stopWatch.Start();
						return;
					}
					if (value == WorkUnitStatus.Completed || value == WorkUnitStatus.Failed)
					{
						this.stopWatch.Stop();
					}
				}
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x000352F4 File Offset: 0x000334F4
		public string Summary
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine(this.Text);
				stringBuilder.AppendLine(LocalizedDescriptionAttribute.FromEnum(typeof(WorkUnitStatus), this.Status));
				if (base.Errors.Count > 0 || base.Warnings.Count > 0)
				{
					stringBuilder.AppendLine(this.ErrorsDescription);
					stringBuilder.AppendLine(this.WarningsDescription);
				}
				else
				{
					stringBuilder.AppendLine(this.Description);
				}
				if (this.Status != WorkUnitStatus.InProgress && this.Status != WorkUnitStatus.NotStarted && this.CanShowExecutedCommand && !string.IsNullOrEmpty(this.ExecutedCommandText))
				{
					stringBuilder.AppendLine(this.ExecutedCommandText);
				}
				if (this.CanShowElapsedTime && this.Status != WorkUnitStatus.NotStarted)
				{
					stringBuilder.AppendLine(this.ElapsedTimeText);
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x000353D0 File Offset: 0x000335D0
		// (set) Token: 0x06001167 RID: 4455 RVA: 0x000353D8 File Offset: 0x000335D8
		[DefaultValue(null)]
		public override object Target
		{
			get
			{
				return base.Target;
			}
			set
			{
				if (base.Target != value)
				{
					base.Target = value;
					this.RaisePropertyChanged("Target");
				}
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06001168 RID: 4456 RVA: 0x000353F5 File Offset: 0x000335F5
		// (set) Token: 0x06001169 RID: 4457 RVA: 0x000353FD File Offset: 0x000335FD
		[DefaultValue("")]
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				value = (value ?? "");
				if (this.Text != value)
				{
					this.text = value;
					this.RaisePropertyChanged("Text");
				}
			}
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x0003542C File Offset: 0x0003362C
		public void ResetStatus()
		{
			this.PercentComplete = 0;
			this.CurrentStatus = 0;
			base.Errors.Clear();
			base.Warnings.Clear();
			this.StatusDescription = "";
			this.ExecutedCommandText = "";
			this.stopWatch.Reset();
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x00035480 File Offset: 0x00033680
		private void RaisePropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x0600116C RID: 4460 RVA: 0x000354A4 File Offset: 0x000336A4
		// (remove) Token: 0x0600116D RID: 4461 RVA: 0x000354DC File Offset: 0x000336DC
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600116E RID: 4462 RVA: 0x00035514 File Offset: 0x00033714
		private WorkUnitStatus ConvertWorkUnitBaseStatus(WorkUnitBaseStatus workUnitBase)
		{
			switch (workUnitBase)
			{
			case 0:
				return WorkUnitStatus.NotStarted;
			case 1:
				return WorkUnitStatus.InProgress;
			case 2:
				return WorkUnitStatus.Completed;
			case 3:
				return WorkUnitStatus.Failed;
			default:
				throw new ArgumentException("workUnitBase");
			}
		}

		// Token: 0x040003DC RID: 988
		private string description;

		// Token: 0x040003DD RID: 989
		private string statusDescription;

		// Token: 0x040003DE RID: 990
		private Icon icon;

		// Token: 0x040003DF RID: 991
		private int percentComplete;

		// Token: 0x040003E0 RID: 992
		private WorkUnitStatus status;

		// Token: 0x040003E1 RID: 993
		private string text;

		// Token: 0x040003E2 RID: 994
		private Stopwatch stopWatch;

		// Token: 0x040003E3 RID: 995
		private bool canShowElapsedTime;

		// Token: 0x040003E4 RID: 996
		private bool canShowExecutedCommand;

		// Token: 0x040003E5 RID: 997
		private string executedCommandText;
	}
}
