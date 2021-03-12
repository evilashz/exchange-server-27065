using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using Microsoft.Exchange.Connections.Common;

namespace Microsoft.Exchange.Data.ApplicationLogic.ConnectionSettingsDiscovery.Connections
{
	// Token: 0x02000195 RID: 405
	internal abstract class ProtocolSpecificConnectionSettings
	{
		// Token: 0x06000F67 RID: 3943 RVA: 0x0003EED8 File Offset: 0x0003D0D8
		protected ProtocolSpecificConnectionSettings(ConnectionSettingsType settingsType)
		{
			this.LogonResult = OperationStatusCode.None;
			this.ConnectionType = settingsType;
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x0003EEEE File Offset: 0x0003D0EE
		// (set) Token: 0x06000F69 RID: 3945 RVA: 0x0003EEF6 File Offset: 0x0003D0F6
		public ConnectionSettingsType ConnectionType { get; private set; }

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x0003EEFF File Offset: 0x0003D0FF
		// (set) Token: 0x06000F6B RID: 3947 RVA: 0x0003EF07 File Offset: 0x0003D107
		public OperationStatusCode LogonResult { get; private set; }

		// Token: 0x06000F6C RID: 3948 RVA: 0x0003EF10 File Offset: 0x0003D110
		public bool TestUserCanLogon(SmtpAddress email, ref string userName, SecureString password)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password", "The password argument cannot be null.");
			}
			if (!email.IsValidAddress)
			{
				throw new ArgumentException("The email argument must have a valid value.", "email");
			}
			foreach (string text in this.FindUserNamesIfNecessary(userName, email))
			{
				OperationStatusCode logonResult = this.TestUserCanLogonWithCurrentSettings(email, text, password);
				this.LogonResult = logonResult;
				if (this.LogonResult == OperationStatusCode.Success)
				{
					userName = text;
					break;
				}
				if (this.LogonResult != OperationStatusCode.ErrorInvalidCredentials)
				{
					break;
				}
			}
			return this.LogonResult == OperationStatusCode.Success;
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0003EFB8 File Offset: 0x0003D1B8
		public virtual string ToMultiLineString(string lineSeparator)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("connection settings:{1}Type={0},{1}", this.ConnectionType, lineSeparator);
			stringBuilder.AppendFormat("Last logon test result={0},{1}", this.LogonResult, lineSeparator);
			return stringBuilder.ToString();
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x0003F001 File Offset: 0x0003D201
		public override string ToString()
		{
			return this.ToMultiLineString(" ");
		}

		// Token: 0x06000F6F RID: 3951
		protected abstract OperationStatusCode TestUserCanLogonWithCurrentSettings(SmtpAddress email, string userName, SecureString password);

		// Token: 0x06000F70 RID: 3952 RVA: 0x0003F148 File Offset: 0x0003D348
		protected virtual IEnumerable<string> FindUserNamesIfNecessary(string userName, SmtpAddress email)
		{
			if (string.IsNullOrEmpty(userName))
			{
				yield return (string)email;
				yield return email.Local;
			}
			else
			{
				yield return userName;
			}
			yield break;
		}
	}
}
