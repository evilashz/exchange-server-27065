using System;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000298 RID: 664
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MsiUIHandler
	{
		// Token: 0x06001805 RID: 6149 RVA: 0x000655DC File Offset: 0x000637DC
		public MsiUIHandler()
		{
			this.UIHandlerDelegate = new MsiUIHandlerDelegate(this.ExternalUIHandler);
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x000655FD File Offset: 0x000637FD
		public int ExternalUIHandler(IntPtr context, uint messageType, string message)
		{
			return this.ExternalUIHandler(messageType, message);
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x00065608 File Offset: 0x00063808
		private int ExternalUIHandler(uint messageType, string message)
		{
			TaskLogger.LogEnter();
			int result = 1;
			InstallMessage installMessage = (InstallMessage)(4278190080U & messageType);
			InstallMessage installMessage2 = installMessage;
			if (installMessage2 <= InstallMessage.FilesInUse)
			{
				if (installMessage2 != InstallMessage.FatalExit)
				{
					if (installMessage2 != InstallMessage.Error)
					{
						if (installMessage2 == InstallMessage.FilesInUse)
						{
							result = 0;
						}
					}
					else if (this.OnMsiError != null)
					{
						this.OnMsiError(message);
					}
				}
			}
			else if (installMessage2 != InstallMessage.OutOfDiskSpace)
			{
				if (installMessage2 != InstallMessage.ActionData)
				{
					if (installMessage2 == InstallMessage.Progress)
					{
						if (this.HandleProgressMessage(this.ParseProgressString(message)))
						{
							this.UpdateProgress();
						}
					}
				}
				else if (this.HandleActionDataMessage())
				{
					this.UpdateProgress();
				}
			}
			else
			{
				result = 0;
			}
			if (this.IsCanceled())
			{
				result = 2;
			}
			TaskLogger.LogExit();
			return result;
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x000656B8 File Offset: 0x000638B8
		private bool HandleActionDataMessage()
		{
			TaskLogger.LogEnter();
			bool result = false;
			if (this.progressTotal != 0 && this.isEnableActionData)
			{
				if (this.isForwardProgress)
				{
					this.progress += this.progressStep;
					if (this.progress > this.progressTotal)
					{
						this.progress = this.progressTotal;
					}
				}
				else
				{
					this.progress -= this.progressStep;
					if (this.progress < 0)
					{
						this.progress = 0;
					}
				}
				result = true;
			}
			TaskLogger.LogExit();
			return result;
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x00065740 File Offset: 0x00063940
		private bool HandleProgressMessage(int[] fields)
		{
			TaskLogger.LogEnter();
			bool result = false;
			if (fields != null && fields.Length == 4)
			{
				switch (fields[0])
				{
				case 0:
					this.progressTotal = fields[1];
					this.progressStep = 0;
					this.isEnableActionData = false;
					this.isForwardProgress = (fields[2] == 0);
					if (!this.isForwardProgress && this.phase == MsiUIHandler.MsiPhase.None)
					{
						this.isProgressEnabled = false;
						this.phase = MsiUIHandler.MsiPhase.Rollback;
					}
					else if (this.isForwardProgress)
					{
						this.progress = 0;
						if (fields[3] == 1)
						{
							if (this.phase == MsiUIHandler.MsiPhase.None || this.phase == MsiUIHandler.MsiPhase.Rollback)
							{
								this.isProgressEnabled = true;
								this.phase = MsiUIHandler.MsiPhase.ScriptGeneration;
							}
							else
							{
								this.isProgressEnabled = false;
							}
						}
						else if (fields[3] == 0)
						{
							if (this.phase == MsiUIHandler.MsiPhase.None || this.phase == MsiUIHandler.MsiPhase.ScriptGeneration)
							{
								this.isProgressEnabled = true;
								this.phase = MsiUIHandler.MsiPhase.ScriptExecution;
							}
							else if (this.phase == MsiUIHandler.MsiPhase.ScriptExecution)
							{
								this.isProgressEnabled = true;
								this.phase = MsiUIHandler.MsiPhase.Cleanup;
							}
							else
							{
								this.isProgressEnabled = false;
							}
						}
						else
						{
							this.isProgressEnabled = false;
						}
						result = true;
					}
					else
					{
						this.progress = this.progressTotal;
						this.phase = MsiUIHandler.MsiPhase.Rollback;
					}
					break;
				case 1:
					if (this.isProgressEnabled)
					{
						if (fields[2] == 1)
						{
							this.progressStep = fields[1];
							this.isEnableActionData = true;
						}
						else
						{
							this.isEnableActionData = false;
						}
					}
					break;
				case 2:
					if (this.isProgressEnabled && fields[1] != 0)
					{
						if (this.isForwardProgress)
						{
							this.progress += fields[1];
						}
						else
						{
							this.progress -= fields[1];
						}
						if (this.progress > this.progressTotal)
						{
							this.progress = this.progressTotal;
						}
						else if (this.progress < 0)
						{
							this.progress = 0;
						}
						result = true;
					}
					break;
				case 3:
					if (this.isProgressEnabled)
					{
						this.progressTotal += fields[1];
						result = true;
					}
					break;
				}
			}
			TaskLogger.LogEnter();
			return result;
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x00065934 File Offset: 0x00063B34
		private int[] ParseProgressString(string progressString)
		{
			TaskLogger.LogEnter();
			int[] array = new int[4];
			int[] array2 = array;
			Regex regex = new Regex("(?<1>\\d):\\s?(?<2>[-+]?\\d+)\\s");
			int num = 0;
			foreach (object obj in regex.Matches(progressString))
			{
				Match match = (Match)obj;
				num = int.Parse(match.Groups[1].Captures[0].Value);
				if (num > 4 || num < 1)
				{
					num = 0;
					break;
				}
				array2[num - 1] = int.Parse(match.Groups[2].Captures[0].Value);
			}
			TaskLogger.LogExit();
			if (num == 0)
			{
				return null;
			}
			return array2;
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x00065A0C File Offset: 0x00063C0C
		private void UpdateProgress()
		{
			TaskLogger.LogEnter();
			if (this.isProgressEnabled)
			{
				int num;
				if (this.progressTotal == 0)
				{
					num = 100;
				}
				else
				{
					num = (int)((long)this.progress * 100L / (long)this.progressTotal);
				}
				switch (this.phase)
				{
				case MsiUIHandler.MsiPhase.ScriptGeneration:
					num = num * 20 / 100;
					break;
				case MsiUIHandler.MsiPhase.ScriptExecution:
					num = num * 70 / 100 + 20;
					break;
				case MsiUIHandler.MsiPhase.Cleanup:
					num = num * 10 / 100 + 20 + 70;
					break;
				}
				if ((num > this.previousPercentage && this.isForwardProgress) || (num < this.previousPercentage && !this.isForwardProgress))
				{
					this.previousPercentage = num;
					this.OnProgress(num);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04000A17 RID: 2583
		private const int GenerationPercentage = 20;

		// Token: 0x04000A18 RID: 2584
		private const int ExecutionPercentage = 70;

		// Token: 0x04000A19 RID: 2585
		private const int CleanupPercentage = 10;

		// Token: 0x04000A1A RID: 2586
		private int progressTotal;

		// Token: 0x04000A1B RID: 2587
		private int progress;

		// Token: 0x04000A1C RID: 2588
		private bool isForwardProgress = true;

		// Token: 0x04000A1D RID: 2589
		private bool isEnableActionData;

		// Token: 0x04000A1E RID: 2590
		private int progressStep;

		// Token: 0x04000A1F RID: 2591
		private bool isProgressEnabled;

		// Token: 0x04000A20 RID: 2592
		private MsiUIHandler.MsiPhase phase;

		// Token: 0x04000A21 RID: 2593
		private int previousPercentage;

		// Token: 0x04000A22 RID: 2594
		public MsiUIHandler.ProgressHandler OnProgress;

		// Token: 0x04000A23 RID: 2595
		public MsiUIHandler.IsCanceledHandler IsCanceled;

		// Token: 0x04000A24 RID: 2596
		public MsiUIHandler.MsiErrorHandler OnMsiError;

		// Token: 0x04000A25 RID: 2597
		public MsiUIHandlerDelegate UIHandlerDelegate;

		// Token: 0x02000299 RID: 665
		private enum MsiPhase
		{
			// Token: 0x04000A27 RID: 2599
			None,
			// Token: 0x04000A28 RID: 2600
			Rollback,
			// Token: 0x04000A29 RID: 2601
			ScriptGeneration,
			// Token: 0x04000A2A RID: 2602
			ScriptExecution,
			// Token: 0x04000A2B RID: 2603
			Cleanup
		}

		// Token: 0x0200029A RID: 666
		// (Invoke) Token: 0x0600180D RID: 6157
		public delegate void ProgressHandler(int progress);

		// Token: 0x0200029B RID: 667
		// (Invoke) Token: 0x06001811 RID: 6161
		public delegate bool IsCanceledHandler();

		// Token: 0x0200029C RID: 668
		// (Invoke) Token: 0x06001815 RID: 6165
		public delegate void MsiErrorHandler(string errorMsg);
	}
}
