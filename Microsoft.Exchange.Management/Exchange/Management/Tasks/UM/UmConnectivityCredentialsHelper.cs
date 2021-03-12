using System;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.Monitoring;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D5F RID: 3423
	internal sealed class UmConnectivityCredentialsHelper
	{
		// Token: 0x0600833E RID: 33598 RVA: 0x00218258 File Offset: 0x00216458
		internal UmConnectivityCredentialsHelper(ADSite site, Server server)
		{
			UmConnectivityCredentialsHelper.DebugTrace("Inside UmConnectivityCredentialsHelper(). ADSite = {0}, Server = {1}", new object[]
			{
				site,
				server
			});
			SmtpAddress? enterpriseAutomatedTaskUser = TestConnectivityCredentialsManager.GetEnterpriseAutomatedTaskUser(site, server.Domain);
			this.userName = TestCasConnectivity.GetInstanceUserNameFromTestUser(enterpriseAutomatedTaskUser);
			this.domain = server.Domain;
		}

		// Token: 0x170028D7 RID: 10455
		// (get) Token: 0x0600833F RID: 33599 RVA: 0x002182AA File Offset: 0x002164AA
		internal ADUser User
		{
			get
			{
				return this.aduser;
			}
		}

		// Token: 0x170028D8 RID: 10456
		// (get) Token: 0x06008340 RID: 33600 RVA: 0x002182B2 File Offset: 0x002164B2
		internal UMDialPlan UserDP
		{
			get
			{
				return this.userDP;
			}
		}

		// Token: 0x170028D9 RID: 10457
		// (get) Token: 0x06008341 RID: 33601 RVA: 0x002182BA File Offset: 0x002164BA
		internal bool IsUserFound
		{
			get
			{
				return this.isUserFound;
			}
		}

		// Token: 0x170028DA RID: 10458
		// (get) Token: 0x06008342 RID: 33602 RVA: 0x002182C2 File Offset: 0x002164C2
		internal bool IsExchangePrincipalFound
		{
			get
			{
				return this.isExchangePrincipalFound;
			}
		}

		// Token: 0x170028DB RID: 10459
		// (get) Token: 0x06008343 RID: 33603 RVA: 0x002182CA File Offset: 0x002164CA
		internal bool IsUserUMEnabled
		{
			get
			{
				return this.isUserUMEnabled;
			}
		}

		// Token: 0x170028DC RID: 10460
		// (get) Token: 0x06008344 RID: 33604 RVA: 0x002182D2 File Offset: 0x002164D2
		internal bool SuccessfullyGotPin
		{
			get
			{
				return this.successfullyGotPin;
			}
		}

		// Token: 0x170028DD RID: 10461
		// (get) Token: 0x06008345 RID: 33605 RVA: 0x002182DA File Offset: 0x002164DA
		internal string UMPin
		{
			get
			{
				return this.umPin;
			}
		}

		// Token: 0x170028DE RID: 10462
		// (get) Token: 0x06008346 RID: 33606 RVA: 0x002182E2 File Offset: 0x002164E2
		internal ExchangePrincipal ExPrincipal
		{
			get
			{
				return this.exp;
			}
		}

		// Token: 0x170028DF RID: 10463
		// (get) Token: 0x06008347 RID: 33607 RVA: 0x002182EA File Offset: 0x002164EA
		internal string UserName
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x170028E0 RID: 10464
		// (get) Token: 0x06008348 RID: 33608 RVA: 0x002182F2 File Offset: 0x002164F2
		internal string UserDomain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x06008349 RID: 33609 RVA: 0x002182FA File Offset: 0x002164FA
		internal static bool IsMailboxServer(Server serv)
		{
			if (serv != null && serv.IsMailboxServer && serv.IsExchange2007OrLater && serv.Id != null)
			{
				Guid objectGuid = serv.Id.ObjectGuid;
				return true;
			}
			return false;
		}

		// Token: 0x0600834A RID: 33610 RVA: 0x00218328 File Offset: 0x00216528
		internal static bool ResetMailboxPassword(ExchangePrincipal ep, NetworkCredential nc)
		{
			UmConnectivityCredentialsHelper.DebugTrace("Inside UmConnectivityCredentialsHelper: ResetMailboxPassword", new object[0]);
			bool flag = false;
			LocalizedException ex = TestConnectivityCredentialsManager.ResetAutomatedCredentialsAndVerify(ep, nc, false, out flag);
			if (ex != null)
			{
				UmConnectivityCredentialsHelper.DebugTrace("Inside ResetMailboxPassword(): TestConnectivityCredentialsManager.ResetAutomatedCredentialsAndVerify returned : {0} ", new object[]
				{
					ex.ToString()
				});
				return false;
			}
			return true;
		}

		// Token: 0x0600834B RID: 33611 RVA: 0x00218374 File Offset: 0x00216574
		internal static bool ResetUMPin(ADUser aduser, string passwd)
		{
			UmConnectivityCredentialsHelper.DebugTrace("Inside UmConnectivityCredentialsHelper: ResetUMPin", new object[0]);
			string pin;
			try
			{
				UMMailboxPolicy policyFromUser = Utility.GetPolicyFromUser(aduser);
				if (!UmConnectivityCredentialsHelper.GetRandomPINFromPasswd(passwd, policyFromUser.MinPINLength, out pin))
				{
					UmConnectivityCredentialsHelper.DebugTrace("Inside ResetUMPin(): didnt get pin", new object[0]);
					return false;
				}
			}
			catch (LocalizedException ex)
			{
				UmConnectivityCredentialsHelper.DebugTrace("Inside ResetUMPin(): got Exception = {0}", new object[]
				{
					ex.ToString()
				});
				return false;
			}
			LocalizedException ex2 = UmConnectivityCredentialsHelper.SaveUMPin(aduser, pin);
			if (ex2 != null)
			{
				UmConnectivityCredentialsHelper.DebugTrace("Inside ResetUMPin(): SaveUMPin Exception = {0}", new object[]
				{
					ex2.ToString()
				});
				return false;
			}
			return true;
		}

		// Token: 0x0600834C RID: 33612 RVA: 0x00218424 File Offset: 0x00216624
		internal static LocalizedException SaveUMPin(ADUser user, string pin)
		{
			try
			{
				Utils.SetUserPassword(user, pin, false, false);
			}
			catch (LocalizedException result)
			{
				return result;
			}
			return null;
		}

		// Token: 0x0600834D RID: 33613 RVA: 0x00218454 File Offset: 0x00216654
		internal void InitializeUser(bool dontFetchPassword)
		{
			UmConnectivityCredentialsHelper.DebugTrace("Inside InitializeUser().", new object[0]);
			this.isUserFound = UmConnectivityCredentialsHelper.FindUser(this.userName, this.domain, out this.aduser);
			if (this.isUserFound)
			{
				UmConnectivityCredentialsHelper.DebugTrace("Inside InitializeUser(). User found", new object[0]);
				this.isUserUMEnabled = this.UserUMEnabled(this.aduser);
			}
			if (this.isUserUMEnabled)
			{
				UmConnectivityCredentialsHelper.DebugTrace("Inside InitializeUser(). User UM Enabled", new object[0]);
				this.isExchangePrincipalFound = UmConnectivityCredentialsHelper.FindExchangePrincipal(this.aduser, out this.exp);
			}
			if (!dontFetchPassword && this.isExchangePrincipalFound)
			{
				UmConnectivityCredentialsHelper.DebugTrace("Inside InitializeUser(). ExchangePrincipal found", new object[0]);
				this.successfullyGotPin = this.GeneratePinFromPassword();
				UmConnectivityCredentialsHelper.DebugTrace("Inside InitializeUser(). successfullyGotPin = {0}", new object[]
				{
					this.successfullyGotPin
				});
			}
		}

		// Token: 0x0600834E RID: 33614 RVA: 0x0021852E File Offset: 0x0021672E
		private static void DebugTrace(string formatString, params object[] formatObjects)
		{
			ExTraceGlobals.DiagnosticTracer.TraceDebug(0L, formatString, formatObjects);
		}

		// Token: 0x0600834F RID: 33615 RVA: 0x00218540 File Offset: 0x00216740
		private static bool GetRandomPINFromPasswd(string passwd, int len, out string pin)
		{
			pin = null;
			int num = Math.Max(len, 10);
			if (passwd == null)
			{
				return false;
			}
			byte[] bytes;
			using (SHA1Cng sha1Cng = new SHA1Cng())
			{
				bytes = sha1Cng.ComputeHash(Encoding.ASCII.GetBytes(passwd));
			}
			StringBuilder stringBuilder = new StringBuilder(Encoding.ASCII.GetString(bytes));
			int length = stringBuilder.Length;
			if (num > length)
			{
				stringBuilder.Append('0', num - length);
			}
			string temp = stringBuilder.ToString().Substring(0, num);
			pin = UmConnectivityCredentialsHelper.GetNumericPinFromString(temp);
			UmConnectivityCredentialsHelper.DebugTrace("Inside GetRandomPINFromPasswd(): pin = {0}", new object[]
			{
				pin
			});
			return true;
		}

		// Token: 0x06008350 RID: 33616 RVA: 0x002185F4 File Offset: 0x002167F4
		private static string GetNumericPinFromString(string temp)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in temp)
			{
				int value = (int)(c % '\n');
				stringBuilder.Append(value);
			}
			UmConnectivityCredentialsHelper.DebugTrace("Inside GetNumericPinFromString(): passed string = {0}, generated numeric pin ={1}", new object[]
			{
				temp,
				stringBuilder.ToString()
			});
			return stringBuilder.ToString();
		}

		// Token: 0x06008351 RID: 33617 RVA: 0x00218660 File Offset: 0x00216860
		private static bool FindPassword(ExchangePrincipal ep, NetworkCredential nc)
		{
			UmConnectivityCredentialsHelper.DebugTrace("Inside FindPassword()", new object[0]);
			LocalizedException ex = TestConnectivityCredentialsManager.LoadAutomatedTestCasConnectivityInfo(ep, nc);
			if (ex != null)
			{
				UmConnectivityCredentialsHelper.DebugTrace("Inside FindPassword(): TestConnectivityCredentialsManager.LoadAutomatedTestCasConnectivityInfo returned : {0}", new object[]
				{
					ex.ToString()
				});
				return false;
			}
			return true;
		}

		// Token: 0x06008352 RID: 33618 RVA: 0x002186A8 File Offset: 0x002168A8
		private static bool FindExchangePrincipal(ADUser user, out ExchangePrincipal ep)
		{
			UmConnectivityCredentialsHelper.DebugTrace("Inside FindExchangePrincipal()", new object[0]);
			ep = null;
			try
			{
				ep = ExchangePrincipal.FromADUser(user, RemotingOptions.AllowCrossSite);
			}
			catch (ObjectNotFoundException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06008353 RID: 33619 RVA: 0x002186EC File Offset: 0x002168EC
		private static bool FindUser(string username, string domain, out ADUser user)
		{
			UmConnectivityCredentialsHelper.DebugTrace("Inside FindUser()", new object[0]);
			user = null;
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.RootOrgOrSingleTenantFromAcceptedDomainAutoDetect(domain), 500, "FindUser", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\um\\UmConnectivityCredentialsHelper.cs");
			string sUserPrincipalName = username + "@" + domain;
			try
			{
				using (WindowsIdentity windowsIdentity = new WindowsIdentity(sUserPrincipalName))
				{
					user = (ADUser)tenantOrRootOrgRecipientSession.FindBySid(windowsIdentity.User);
				}
			}
			catch (SecurityException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
			catch (ArgumentException)
			{
			}
			catch (LocalizedException)
			{
			}
			if (user == null)
			{
				string accountName = domain + "\\" + username;
				try
				{
					user = (ADUser)tenantOrRootOrgRecipientSession.FindByAccountName<ADRecipient>(domain, accountName);
				}
				catch (LocalizedException)
				{
				}
			}
			if (user == null)
			{
				if (username.Length > 20)
				{
					username = username.Substring(0, 20);
				}
				try
				{
					user = (ADUser)tenantOrRootOrgRecipientSession.FindByAccountName<ADRecipient>(domain, username);
				}
				catch (LocalizedException)
				{
				}
			}
			return user != null;
		}

		// Token: 0x06008354 RID: 33620 RVA: 0x00218820 File Offset: 0x00216A20
		private bool GeneratePinFromPassword()
		{
			UmConnectivityCredentialsHelper.DebugTrace("Inside GeneratePinFromPassword()", new object[0]);
			NetworkCredential networkCredential = new NetworkCredential(this.userName, string.Empty, this.domain);
			if (!UmConnectivityCredentialsHelper.FindPassword(this.exp, networkCredential))
			{
				UmConnectivityCredentialsHelper.DebugTrace("Inside GeneratePinFromPassword(): didnt find passwd", new object[0]);
				return false;
			}
			try
			{
				UMMailboxPolicy policyFromUser = Utility.GetPolicyFromUser(this.aduser);
				if (!UmConnectivityCredentialsHelper.GetRandomPINFromPasswd(networkCredential.Password, policyFromUser.MinPINLength, out this.umPin))
				{
					UmConnectivityCredentialsHelper.DebugTrace("Inside GeneratePinFromPassword(): didnt get pin", new object[0]);
					return false;
				}
			}
			catch (LocalizedException ex)
			{
				UmConnectivityCredentialsHelper.DebugTrace("Inside GeneratePinFromPassword(): got Exception = {0}", new object[]
				{
					ex.ToString()
				});
				return false;
			}
			return true;
		}

		// Token: 0x06008355 RID: 33621 RVA: 0x002188E8 File Offset: 0x00216AE8
		private bool UserUMEnabled(ADUser user)
		{
			UmConnectivityCredentialsHelper.DebugTrace("UmConnectivityCredentialsHelper::UserUMEnabled()", new object[0]);
			if (user != null)
			{
				using (UMSubscriber umsubscriber = UMRecipient.Factory.FromADRecipient<UMSubscriber>(user))
				{
					if (umsubscriber != null)
					{
						this.userDP = umsubscriber.DialPlan;
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x04003FB9 RID: 16313
		private const int TestMailboxUMPinLengthMin = 10;

		// Token: 0x04003FBA RID: 16314
		private readonly string userName;

		// Token: 0x04003FBB RID: 16315
		private readonly string domain;

		// Token: 0x04003FBC RID: 16316
		private ADUser aduser;

		// Token: 0x04003FBD RID: 16317
		private bool isUserFound;

		// Token: 0x04003FBE RID: 16318
		private bool isUserUMEnabled;

		// Token: 0x04003FBF RID: 16319
		private bool isExchangePrincipalFound;

		// Token: 0x04003FC0 RID: 16320
		private ExchangePrincipal exp;

		// Token: 0x04003FC1 RID: 16321
		private bool successfullyGotPin;

		// Token: 0x04003FC2 RID: 16322
		private string umPin;

		// Token: 0x04003FC3 RID: 16323
		private UMDialPlan userDP;
	}
}
