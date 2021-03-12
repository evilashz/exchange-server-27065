using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200105A RID: 4186
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ErrorUnableToFindFqdnForHostADErrorException : LocalizedException
	{
		// Token: 0x0600B092 RID: 45202 RVA: 0x0029666D File Offset: 0x0029486D
		public ErrorUnableToFindFqdnForHostADErrorException(string computerName, string ex) : base(Strings.ErrorUnableToFindFqdnForHostADErrorException(computerName, ex))
		{
			this.computerName = computerName;
			this.ex = ex;
		}

		// Token: 0x0600B093 RID: 45203 RVA: 0x0029668A File Offset: 0x0029488A
		public ErrorUnableToFindFqdnForHostADErrorException(string computerName, string ex, Exception innerException) : base(Strings.ErrorUnableToFindFqdnForHostADErrorException(computerName, ex), innerException)
		{
			this.computerName = computerName;
			this.ex = ex;
		}

		// Token: 0x0600B094 RID: 45204 RVA: 0x002966A8 File Offset: 0x002948A8
		protected ErrorUnableToFindFqdnForHostADErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.computerName = (string)info.GetValue("computerName", typeof(string));
			this.ex = (string)info.GetValue("ex", typeof(string));
		}

		// Token: 0x0600B095 RID: 45205 RVA: 0x002966FD File Offset: 0x002948FD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("computerName", this.computerName);
			info.AddValue("ex", this.ex);
		}

		// Token: 0x17003847 RID: 14407
		// (get) Token: 0x0600B096 RID: 45206 RVA: 0x00296729 File Offset: 0x00294929
		public string ComputerName
		{
			get
			{
				return this.computerName;
			}
		}

		// Token: 0x17003848 RID: 14408
		// (get) Token: 0x0600B097 RID: 45207 RVA: 0x00296731 File Offset: 0x00294931
		public string Ex
		{
			get
			{
				return this.ex;
			}
		}

		// Token: 0x040061AD RID: 25005
		private readonly string computerName;

		// Token: 0x040061AE RID: 25006
		private readonly string ex;
	}
}
