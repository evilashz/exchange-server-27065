using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200019F RID: 415
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class AuthenticationMethodNotSupportedException : MigrationPermanentException
	{
		// Token: 0x0600176C RID: 5996 RVA: 0x00070A5D File Offset: 0x0006EC5D
		public AuthenticationMethodNotSupportedException(string authenticationMethod, string protocol, string validValues) : base(Strings.ErrorAuthenticationMethodNotSupported(authenticationMethod, protocol, validValues))
		{
			this.authenticationMethod = authenticationMethod;
			this.protocol = protocol;
			this.validValues = validValues;
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x00070A82 File Offset: 0x0006EC82
		public AuthenticationMethodNotSupportedException(string authenticationMethod, string protocol, string validValues, Exception innerException) : base(Strings.ErrorAuthenticationMethodNotSupported(authenticationMethod, protocol, validValues), innerException)
		{
			this.authenticationMethod = authenticationMethod;
			this.protocol = protocol;
			this.validValues = validValues;
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x00070AAC File Offset: 0x0006ECAC
		protected AuthenticationMethodNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.authenticationMethod = (string)info.GetValue("authenticationMethod", typeof(string));
			this.protocol = (string)info.GetValue("protocol", typeof(string));
			this.validValues = (string)info.GetValue("validValues", typeof(string));
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x00070B21 File Offset: 0x0006ED21
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("authenticationMethod", this.authenticationMethod);
			info.AddValue("protocol", this.protocol);
			info.AddValue("validValues", this.validValues);
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06001770 RID: 6000 RVA: 0x00070B5E File Offset: 0x0006ED5E
		public string AuthenticationMethod
		{
			get
			{
				return this.authenticationMethod;
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06001771 RID: 6001 RVA: 0x00070B66 File Offset: 0x0006ED66
		public string Protocol
		{
			get
			{
				return this.protocol;
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06001772 RID: 6002 RVA: 0x00070B6E File Offset: 0x0006ED6E
		public string ValidValues
		{
			get
			{
				return this.validValues;
			}
		}

		// Token: 0x04000B18 RID: 2840
		private readonly string authenticationMethod;

		// Token: 0x04000B19 RID: 2841
		private readonly string protocol;

		// Token: 0x04000B1A RID: 2842
		private readonly string validValues;
	}
}
