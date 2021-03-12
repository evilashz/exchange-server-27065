using System;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Isam.Esent.Interop;
using Microsoft.Isam.Esent.Interop.Unpublished;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200011C RID: 284
	internal class LogVerifier
	{
		// Token: 0x06000ACD RID: 2765 RVA: 0x00030884 File Offset: 0x0002EA84
		private LogVerifier(string basename, bool ignored)
		{
			bool flag = false;
			EseHelper.GlobalInit();
			this.m_instanceName = string.Concat(new object[]
			{
				"Log Verifier ",
				basename,
				" ",
				this.GetHashCode()
			});
			Api.JetCreateInstance(out this.m_instance, this.m_instanceName);
			this.m_basename = basename;
			bool jettermNeeded = true;
			try
			{
				InstanceParameters instanceParameters = new InstanceParameters(this.Instance);
				instanceParameters.Recovery = false;
				instanceParameters.MaxTemporaryTables = 0;
				instanceParameters.NoInformationEvent = true;
				instanceParameters.EventLoggingLevel = EventLoggingLevels.Min;
				instanceParameters.BaseName = basename;
				jettermNeeded = false;
				Api.JetInit(ref this.m_instance);
				jettermNeeded = true;
				Api.JetBeginSession(this.Instance, out this.m_sesid, null, null);
				this.m_dbutil = new JET_DBUTIL();
				this.m_dbutil.sesid = this.Sesid;
				this.m_dbutil.op = DBUTIL_OP.DumpLogfile;
				this.AssertNotTerminated();
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.Term(jettermNeeded);
				}
			}
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0003098C File Offset: 0x0002EB8C
		public LogVerifier(string basename, string csvFile) : this(basename, true)
		{
			this.m_dbutil.grbitOptions = (DbutilGrbit.OptionDumpLogInfoCSV | DbutilGrbit.OptionDumpLogPermitPatching);
			this.m_dbutil.szIntegPrefix = csvFile;
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x000309B2 File Offset: 0x0002EBB2
		public LogVerifier(string basename) : this(basename, true)
		{
			this.m_dbutil.grbitOptions = (DbutilGrbit.OptionVerify | DbutilGrbit.OptionDumpLogPermitPatching);
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x000309CC File Offset: 0x0002EBCC
		public static Exception Verify(string fileName, string logFilePrefix, long expectedGen, JET_SIGNATURE? expectedLogSignature)
		{
			LogVerifier logVerifier = new LogVerifier(logFilePrefix);
			EsentErrorException ex = logVerifier.Verify(fileName);
			if (ex != null)
			{
				return ex;
			}
			JET_LOGINFOMISC jet_LOGINFOMISC;
			UnpublishedApi.JetGetLogFileInfo(fileName, out jet_LOGINFOMISC, JET_LogInfo.Misc2);
			if ((long)jet_LOGINFOMISC.ulGeneration != expectedGen)
			{
				return new FileCheckLogfileGenerationException(fileName, (long)jet_LOGINFOMISC.ulGeneration, expectedGen);
			}
			if (expectedLogSignature != null && !jet_LOGINFOMISC.signLog.Equals(expectedLogSignature))
			{
				return new FileCheckLogfileSignatureException(fileName, jet_LOGINFOMISC.signLog.ToString(), expectedLogSignature.ToString());
			}
			return null;
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00030A5E File Offset: 0x0002EC5E
		public void Term()
		{
			this.Term(true);
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00030A68 File Offset: 0x0002EC68
		private void Term(bool jettermNeeded)
		{
			lock (this)
			{
				this.AssertNotTerminated();
				if (jettermNeeded)
				{
					Api.JetTerm(this.Instance);
				}
				this.m_terminated = true;
			}
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00030AB8 File Offset: 0x0002ECB8
		public void Dump(string logfile)
		{
			this.AssertNotTerminated();
			this.m_dbutil.szDatabase = logfile;
			ExTraceGlobals.EseutilWrapperTracer.TraceDebug<string>((long)this.GetHashCode(), "Dumping {0}", logfile);
			UnpublishedApi.JetDBUtilities(this.Dbutil);
			ExTraceGlobals.EseutilWrapperTracer.TraceDebug<string>((long)this.GetHashCode(), "Dumping of logfile {0} detected no errors", logfile);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00030B10 File Offset: 0x0002ED10
		public EsentErrorException Verify(string logfile)
		{
			lock (this)
			{
				this.AssertNotTerminated();
				this.m_dbutil.szDatabase = logfile;
				try
				{
					ExTraceGlobals.EseutilWrapperTracer.TraceDebug<string>((long)this.GetHashCode(), "Verifying {0}", logfile);
					UnpublishedApi.JetDBUtilities(this.Dbutil);
					ExTraceGlobals.EseutilWrapperTracer.TraceDebug<string>((long)this.GetHashCode(), "Verification of logfile {0} detected no errors", logfile);
					ExTraceGlobals.PFDTracer.TracePfd<int, string>((long)this.GetHashCode(), "PFD CRS {0} Verification of logfile {1} detected no errors", 31211, logfile);
				}
				catch (EsentErrorException ex)
				{
					ExTraceGlobals.EseutilWrapperTracer.TraceError<string, EsentErrorException>((long)this.GetHashCode(), "Verification of logfile {0} detected corruption: {1}", logfile, ex);
					return ex;
				}
			}
			return null;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00030BDC File Offset: 0x0002EDDC
		private void AssertNotTerminated()
		{
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x00030BDE File Offset: 0x0002EDDE
		private JET_INSTANCE Instance
		{
			get
			{
				this.AssertNotTerminated();
				return this.m_instance;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x00030BEC File Offset: 0x0002EDEC
		private JET_SESID Sesid
		{
			get
			{
				this.AssertNotTerminated();
				return this.m_sesid;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x00030BFA File Offset: 0x0002EDFA
		private JET_DBUTIL Dbutil
		{
			get
			{
				this.AssertNotTerminated();
				return this.m_dbutil;
			}
		}

		// Token: 0x04000491 RID: 1169
		private readonly JET_INSTANCE m_instance;

		// Token: 0x04000492 RID: 1170
		private readonly JET_SESID m_sesid;

		// Token: 0x04000493 RID: 1171
		private readonly string m_basename;

		// Token: 0x04000494 RID: 1172
		private JET_DBUTIL m_dbutil;

		// Token: 0x04000495 RID: 1173
		private bool m_terminated;

		// Token: 0x04000496 RID: 1174
		private readonly string m_instanceName;
	}
}
