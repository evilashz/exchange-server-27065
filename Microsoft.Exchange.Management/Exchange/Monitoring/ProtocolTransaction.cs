using System;
using System.Collections;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200051B RID: 1307
	internal abstract class ProtocolTransaction
	{
		// Token: 0x06002F2B RID: 12075 RVA: 0x000BE447 File Offset: 0x000BC647
		internal ProtocolTransaction()
		{
			this.stopwatch = new Stopwatch();
		}

		// Token: 0x06002F2C RID: 12076 RVA: 0x000BE45C File Offset: 0x000BC65C
		internal ProtocolTransaction(ADUser user, string serverName, string password, int port)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			if (string.IsNullOrEmpty(serverName))
			{
				throw new ArgumentNullException("server");
			}
			if (string.IsNullOrEmpty(password))
			{
				throw new ArgumentNullException("password");
			}
			this.user = user;
			this.casServer = serverName;
			this.password = password;
			this.port = port;
			this.stopwatch = new Stopwatch();
		}

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x06002F2D RID: 12077 RVA: 0x000BE4CB File Offset: 0x000BC6CB
		// (set) Token: 0x06002F2E RID: 12078 RVA: 0x000BE4D3 File Offset: 0x000BC6D3
		internal ProtocolConnectionType ConnectionType
		{
			get
			{
				return this.connectionType;
			}
			set
			{
				this.connectionType = value;
			}
		}

		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x06002F2F RID: 12079 RVA: 0x000BE4DC File Offset: 0x000BC6DC
		// (set) Token: 0x06002F30 RID: 12080 RVA: 0x000BE4E4 File Offset: 0x000BC6E4
		internal bool TrustAnySslCertificate
		{
			get
			{
				return this.trustAnySslCertificate;
			}
			set
			{
				this.trustAnySslCertificate = value;
			}
		}

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x06002F31 RID: 12081 RVA: 0x000BE4ED File Offset: 0x000BC6ED
		// (set) Token: 0x06002F32 RID: 12082 RVA: 0x000BE4F5 File Offset: 0x000BC6F5
		internal string MailSubject
		{
			get
			{
				return this.mailSubject;
			}
			set
			{
				this.mailSubject = value;
			}
		}

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06002F33 RID: 12083 RVA: 0x000BE4FE File Offset: 0x000BC6FE
		// (set) Token: 0x06002F34 RID: 12084 RVA: 0x000BE506 File Offset: 0x000BC706
		internal CasTransactionOutcome TransactionResult
		{
			get
			{
				return this.transactionResult;
			}
			set
			{
				this.transactionResult = value;
			}
		}

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x06002F35 RID: 12085 RVA: 0x000BE50F File Offset: 0x000BC70F
		internal Queue InstanceQueue
		{
			get
			{
				return this.instance.Outcomes;
			}
		}

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06002F36 RID: 12086 RVA: 0x000BE51C File Offset: 0x000BC71C
		// (set) Token: 0x06002F37 RID: 12087 RVA: 0x000BE524 File Offset: 0x000BC724
		internal TestCasConnectivity.TestCasConnectivityRunInstance Instance
		{
			get
			{
				return this.instance;
			}
			set
			{
				this.instance = value;
			}
		}

		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x06002F38 RID: 12088 RVA: 0x000BE52D File Offset: 0x000BC72D
		internal bool LightMode
		{
			get
			{
				return this.instance.LightMode;
			}
		}

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x06002F39 RID: 12089 RVA: 0x000BE53A File Offset: 0x000BC73A
		protected ADUser User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x06002F3A RID: 12090 RVA: 0x000BE542 File Offset: 0x000BC742
		protected string CasServer
		{
			get
			{
				return this.casServer;
			}
		}

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x06002F3B RID: 12091 RVA: 0x000BE54A File Offset: 0x000BC74A
		protected string Password
		{
			get
			{
				return this.password;
			}
		}

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x06002F3C RID: 12092 RVA: 0x000BE552 File Offset: 0x000BC752
		protected int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x06002F3D RID: 12093 RVA: 0x000BE55A File Offset: 0x000BC75A
		// (set) Token: 0x06002F3E RID: 12094 RVA: 0x000BE562 File Offset: 0x000BC762
		protected Stopwatch LatencyTimer
		{
			get
			{
				return this.stopwatch;
			}
			set
			{
				this.stopwatch = value;
			}
		}

		// Token: 0x06002F3F RID: 12095
		internal abstract void Execute();

		// Token: 0x06002F40 RID: 12096 RVA: 0x000BE56B File Offset: 0x000BC76B
		protected void Verbose(object message)
		{
			this.instance.Outcomes.Enqueue(message);
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x000BE580 File Offset: 0x000BC780
		protected bool IsServiceRunning(string serviceName, string server)
		{
			using (ServiceController serviceController = new ServiceController(serviceName, server))
			{
				if (serviceController.Status != ServiceControllerStatus.Running)
				{
					this.transactionResult.Update(CasTransactionResultEnum.Failure, TimeSpan.Zero, Strings.ServiceNotRunning(serviceName));
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002F42 RID: 12098 RVA: 0x000BE5E0 File Offset: 0x000BC7E0
		protected void UpdateTransactionResults(CasTransactionResultEnum status, object messageObject)
		{
			string additionalInformation;
			if (messageObject == null)
			{
				additionalInformation = string.Empty;
			}
			else if (messageObject is Exception)
			{
				additionalInformation = ProtocolTransaction.FormatExceptionOutput((Exception)messageObject);
			}
			else
			{
				additionalInformation = messageObject.ToString();
			}
			this.transactionResult.Update(status, this.stopwatch.Elapsed, additionalInformation);
		}

		// Token: 0x06002F43 RID: 12099 RVA: 0x000BE630 File Offset: 0x000BC830
		protected void CompleteTransaction()
		{
			this.stopwatch.Stop();
			this.instance.Outcomes.Enqueue(this.transactionResult);
			this.instance.Result.Outcomes.Add(this.transactionResult);
			this.instance.Result.Complete();
		}

		// Token: 0x06002F44 RID: 12100 RVA: 0x000BE68C File Offset: 0x000BC88C
		private static string FormatExceptionOutput(Exception exception)
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			for (Exception ex = exception; ex != null; ex = ex.InnerException)
			{
				stringBuilder.AppendFormat("{0}: {1}", ex.GetType(), (ex.Message != null) ? ex.Message : string.Empty);
				if (ex.InnerException != null)
				{
					stringBuilder.Append(" ---> ");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040021B9 RID: 8633
		protected const string POPService = "MSExchangePOP3";

		// Token: 0x040021BA RID: 8634
		protected const string IMAPService = "MSExchangeIMAP4";

		// Token: 0x040021BB RID: 8635
		protected const string IISService = "IISADMIN";

		// Token: 0x040021BC RID: 8636
		private const string DateClause = "\r\nDate:";

		// Token: 0x040021BD RID: 8637
		private const string SubjectClause = "\r\nSubject:";

		// Token: 0x040021BE RID: 8638
		private const char RChar = '\r';

		// Token: 0x040021BF RID: 8639
		private string mailSubject;

		// Token: 0x040021C0 RID: 8640
		private readonly int port;

		// Token: 0x040021C1 RID: 8641
		private ADUser user;

		// Token: 0x040021C2 RID: 8642
		private readonly string password;

		// Token: 0x040021C3 RID: 8643
		private readonly string casServer;

		// Token: 0x040021C4 RID: 8644
		private ProtocolConnectionType connectionType;

		// Token: 0x040021C5 RID: 8645
		private bool trustAnySslCertificate;

		// Token: 0x040021C6 RID: 8646
		private CasTransactionOutcome transactionResult;

		// Token: 0x040021C7 RID: 8647
		private TestCasConnectivity.TestCasConnectivityRunInstance instance;

		// Token: 0x040021C8 RID: 8648
		private Stopwatch stopwatch;
	}
}
