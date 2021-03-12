using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200041A RID: 1050
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ArgumentNotSupportedException : DiagnosticArgumentException
	{
		// Token: 0x06001958 RID: 6488 RVA: 0x0005F8F2 File Offset: 0x0005DAF2
		public ArgumentNotSupportedException(string argumentName, string supportedArguments) : base(DiagnosticsResources.ArgumentNotSupported(argumentName, supportedArguments))
		{
			this.argumentName = argumentName;
			this.supportedArguments = supportedArguments;
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x0005F90F File Offset: 0x0005DB0F
		public ArgumentNotSupportedException(string argumentName, string supportedArguments, Exception innerException) : base(DiagnosticsResources.ArgumentNotSupported(argumentName, supportedArguments), innerException)
		{
			this.argumentName = argumentName;
			this.supportedArguments = supportedArguments;
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x0005F930 File Offset: 0x0005DB30
		protected ArgumentNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.argumentName = (string)info.GetValue("argumentName", typeof(string));
			this.supportedArguments = (string)info.GetValue("supportedArguments", typeof(string));
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x0005F985 File Offset: 0x0005DB85
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("argumentName", this.argumentName);
			info.AddValue("supportedArguments", this.supportedArguments);
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x0600195C RID: 6492 RVA: 0x0005F9B1 File Offset: 0x0005DBB1
		public string ArgumentName
		{
			get
			{
				return this.argumentName;
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x0600195D RID: 6493 RVA: 0x0005F9B9 File Offset: 0x0005DBB9
		public string SupportedArguments
		{
			get
			{
				return this.supportedArguments;
			}
		}

		// Token: 0x04001DEC RID: 7660
		private readonly string argumentName;

		// Token: 0x04001DED RID: 7661
		private readonly string supportedArguments;
	}
}
