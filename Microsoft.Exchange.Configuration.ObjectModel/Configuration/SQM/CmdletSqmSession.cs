using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Sqm;

namespace Microsoft.Exchange.Configuration.SQM
{
	// Token: 0x02000267 RID: 615
	internal sealed class CmdletSqmSession : SqmSession
	{
		// Token: 0x0600154E RID: 5454 RVA: 0x0004EC5C File Offset: 0x0004CE5C
		private CmdletSqmSession() : base(SqmAppID.Configuration, SqmSession.Scope.AppDomain)
		{
			base.Open();
			CmdletSqmSession.SetConsoleCtrlHandler(CmdletSqmSession.breakHandler, true);
			AppDomain.CurrentDomain.ProcessExit += CmdletSqmSession.CloseSessionEventHandler;
			AppDomain.CurrentDomain.DomainUnload += delegate(object param0, EventArgs param1)
			{
				CmdletSqmSession.CloseSessionEventHandler(null, EventArgs.Empty);
				AppDomain.CurrentDomain.ProcessExit -= CmdletSqmSession.CloseSessionEventHandler;
			};
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x0004ECCC File Offset: 0x0004CECC
		public static CmdletSqmSession Instance
		{
			get
			{
				if (CmdletSqmSession.instance == null)
				{
					CmdletSqmSession.instance = new CmdletSqmSession();
				}
				return CmdletSqmSession.instance;
			}
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x0004ECE4 File Offset: 0x0004CEE4
		public static bool BreakHandler(int dwCtrlType)
		{
			switch (dwCtrlType)
			{
			case 1:
			case 2:
			case 5:
			case 6:
				if (CmdletSqmSession.Instance != null)
				{
					CmdletSqmSession.Instance.Close();
				}
				break;
			}
			return false;
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x0004ED28 File Offset: 0x0004CF28
		public void WriteSQMSession(string cmdletName, string[] paraNames, string context, uint iterationNumber, uint runtime, SqmErrorRecord[] errors)
		{
			lock (this.mutex)
			{
				Guid guid = Guid.NewGuid();
				base.AddToStreamDataPoint(SqmDataID.CMDLET_INFRA_CMDLETINFO, new object[]
				{
					cmdletName,
					iterationNumber,
					runtime,
					context,
					(uint)guid.GetHashCode()
				});
				foreach (string text in paraNames)
				{
					base.AddToStreamDataPoint(SqmDataID.CMDLET_INFRA_PARAMETER_NAME, new object[]
					{
						text,
						(uint)guid.GetHashCode()
					});
				}
				foreach (SqmErrorRecord sqmErrorRecord in errors)
				{
					base.AddToStreamDataPoint(SqmDataID.CMDLET_INFRA_ERROR_NAME, new object[]
					{
						sqmErrorRecord.ExceptionType,
						(uint)guid.GetHashCode(),
						sqmErrorRecord.ErrorId,
						context
					});
				}
			}
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x0004EE70 File Offset: 0x0004D070
		private static void CloseSessionEventHandler(object sender, EventArgs e)
		{
			CmdletSqmSession.Instance.Close();
		}

		// Token: 0x06001553 RID: 5459
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern bool SetConsoleCtrlHandler(CmdletSqmSession.ConsoleCtrlDelegate HandlerRoutine, bool Add);

		// Token: 0x04000668 RID: 1640
		public const int SqmErrorNumberPerCmdlet = 6;

		// Token: 0x04000669 RID: 1641
		private const int CTRL_C_EVENT = 0;

		// Token: 0x0400066A RID: 1642
		private const int CTRL_BREAK_EVENT = 1;

		// Token: 0x0400066B RID: 1643
		private const int CTRL_CLOSE_EVENT = 2;

		// Token: 0x0400066C RID: 1644
		private const int CTRL_LOGOFF_EVENT = 5;

		// Token: 0x0400066D RID: 1645
		private const int CTRL_SHUTDOWN_EVENT = 6;

		// Token: 0x0400066E RID: 1646
		private static CmdletSqmSession.ConsoleCtrlDelegate breakHandler = new CmdletSqmSession.ConsoleCtrlDelegate(CmdletSqmSession.BreakHandler);

		// Token: 0x0400066F RID: 1647
		private static CmdletSqmSession instance = null;

		// Token: 0x04000670 RID: 1648
		private object mutex = new object();

		// Token: 0x02000268 RID: 616
		// (Invoke) Token: 0x06001557 RID: 5463
		public delegate bool ConsoleCtrlDelegate(int dwCtrlType);
	}
}
