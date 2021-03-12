using System;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001E2 RID: 482
	internal class WarningReportEventArgs : RunGuidEventArgs, IDisposable
	{
		// Token: 0x06001143 RID: 4419 RVA: 0x00034DC1 File Offset: 0x00032FC1
		public WarningReportEventArgs(Guid guid, string warning, int objectIndex, MonadCommand command) : base(guid)
		{
			if (warning == null)
			{
				throw new ArgumentNullException("warning");
			}
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			this.SetWarningMessageHelpUrl(warning);
			this.objectIndex = objectIndex;
			this.command = command;
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x00034DFD File Offset: 0x00032FFD
		public string WarningMessage
		{
			get
			{
				return this.warningMessage;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x00034E05 File Offset: 0x00033005
		public string HelpUrl
		{
			get
			{
				return this.helpUrl;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x00034E0D File Offset: 0x0003300D
		public int ObjectIndex
		{
			get
			{
				return this.objectIndex;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06001147 RID: 4423 RVA: 0x00034E15 File Offset: 0x00033015
		public MonadCommand Command
		{
			get
			{
				return this.command;
			}
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x00034E1D File Offset: 0x0003301D
		public static string CombineWarningMessageHelpUrl(string warningMessage, string helpUrl)
		{
			if (warningMessage == null)
			{
				throw new ArgumentNullException("warningMessage");
			}
			if (helpUrl == null)
			{
				throw new ArgumentNullException("helpUrl");
			}
			return string.Format("{0}{1}{2}", warningMessage, " For more information see the help link: ", helpUrl);
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x00034E4C File Offset: 0x0003304C
		public void Dispose()
		{
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x00034E50 File Offset: 0x00033050
		private void SetWarningMessageHelpUrl(string warning)
		{
			int num = warning.IndexOf(" For more information see the help link: ");
			if (num >= 0)
			{
				this.warningMessage = warning.Substring(0, num);
				this.helpUrl = warning.Substring(num + " For more information see the help link: ".Length);
				return;
			}
			this.warningMessage = warning;
			this.helpUrl = string.Empty;
		}

		// Token: 0x040003D7 RID: 983
		private const string HelpUrlSeparator = " For more information see the help link: ";

		// Token: 0x040003D8 RID: 984
		private string warningMessage;

		// Token: 0x040003D9 RID: 985
		private string helpUrl;

		// Token: 0x040003DA RID: 986
		private int objectIndex;

		// Token: 0x040003DB RID: 987
		private MonadCommand command;
	}
}
