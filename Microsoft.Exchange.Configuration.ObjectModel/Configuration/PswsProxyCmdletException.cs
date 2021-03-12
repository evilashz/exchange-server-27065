using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;

namespace Microsoft.Exchange.Configuration
{
	// Token: 0x020002E2 RID: 738
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PswsProxyCmdletException : PswsProxyException
	{
		// Token: 0x060019CE RID: 6606 RVA: 0x0005D9FD File Offset: 0x0005BBFD
		public PswsProxyCmdletException(string errorMessage) : base(Strings.PswsCmdletError(errorMessage))
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x0005DA12 File Offset: 0x0005BC12
		public PswsProxyCmdletException(string errorMessage, Exception innerException) : base(Strings.PswsCmdletError(errorMessage), innerException)
		{
			this.errorMessage = errorMessage;
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x0005DA28 File Offset: 0x0005BC28
		protected PswsProxyCmdletException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorMessage = (string)info.GetValue("errorMessage", typeof(string));
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x0005DA52 File Offset: 0x0005BC52
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorMessage", this.errorMessage);
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060019D2 RID: 6610 RVA: 0x0005DA6D File Offset: 0x0005BC6D
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x040009AB RID: 2475
		private readonly string errorMessage;
	}
}
