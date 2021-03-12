using System;
using System.Collections;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Common.LocStrings;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200028D RID: 653
	internal class SetupLoggingModule : ITaskModule, ICriticalFeature
	{
		// Token: 0x0600167D RID: 5757 RVA: 0x0005520E File Offset: 0x0005340E
		public SetupLoggingModule(TaskContext context)
		{
			this.context = context;
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x0005521D File Offset: 0x0005341D
		bool ICriticalFeature.IsCriticalException(Exception ex)
		{
			return false;
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x00055220 File Offset: 0x00053420
		public void Init(ITaskEvent task)
		{
			if (this.context.InvocationInfo.IsVerboseOn && !string.Equals(this.context.InvocationInfo.CommandName, "Write-ExchangeSetupLog", StringComparison.OrdinalIgnoreCase))
			{
				task.PreInit += this.WriteCommandParams;
				task.PreRelease += this.ResetTaskLoggerSetting;
			}
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x00055280 File Offset: 0x00053480
		public void Dispose()
		{
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x00055284 File Offset: 0x00053484
		private void WriteCommandParams(object sender, EventArgs e)
		{
			TaskInvocationInfo invocationInfo = this.context.InvocationInfo;
			this.savedLogAllAsInfo = TaskLogger.LogAllAsInfo;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in invocationInfo.UserSpecifiedParameters.Keys)
			{
				string text = (string)obj;
				object obj2 = invocationInfo.UserSpecifiedParameters[text];
				if (obj2 != null)
				{
					try
					{
						stringBuilder.AppendFormat(" -{0}:{1}", text, SetupLoggingModule.GetPSValue(obj2));
						goto IL_8C;
					}
					catch (Exception e2)
					{
						this.context.CommandShell.WriteVerbose(Strings.VerboseTaskParameterLoggingFailed(text, e2));
						goto IL_8C;
					}
					goto IL_7F;
				}
				goto IL_7F;
				IL_8C:
				if (string.Equals(text, "ErrorAction", StringComparison.OrdinalIgnoreCase) && (ActionPreference)obj2 == ActionPreference.SilentlyContinue)
				{
					TaskLogger.LogAllAsInfo = true;
					continue;
				}
				continue;
				IL_7F:
				stringBuilder.AppendFormat(" -{0}:$null", text);
				goto IL_8C;
			}
			this.context.CommandShell.WriteVerbose(Strings.VerboseTaskSpecifiedParameters(stringBuilder.ToString()));
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x00055390 File Offset: 0x00053590
		private void ResetTaskLoggerSetting(object sender, EventArgs e)
		{
			TaskLogger.LogAllAsInfo = this.savedLogAllAsInfo;
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x0005539D File Offset: 0x0005359D
		private static string GetPSValue(object value)
		{
			return SetupLoggingModule.GetPSValue(value, false);
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x000553A8 File Offset: 0x000535A8
		private static string GetPSValue(object value, bool decorateArray)
		{
			if (value == null)
			{
				return "$null";
			}
			if (value is IEnumerable && !(value is string))
			{
				bool flag = true;
				StringBuilder stringBuilder = new StringBuilder();
				if (decorateArray)
				{
					stringBuilder.Append("@(");
				}
				foreach (object value2 in ((IEnumerable)value))
				{
					if (!flag)
					{
						stringBuilder.Append(',');
					}
					flag = false;
					stringBuilder.Append(SetupLoggingModule.GetPSValue(value2, true));
				}
				if (decorateArray)
				{
					stringBuilder.Append(")");
				}
				return stringBuilder.ToString();
			}
			return string.Format("'{0}'", value.ToString().Replace("'", "''"));
		}

		// Token: 0x040006DF RID: 1759
		private readonly TaskContext context;

		// Token: 0x040006E0 RID: 1760
		private bool savedLogAllAsInfo;
	}
}
