using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011A9 RID: 4521
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToSetMSSRegistryValueException : LocalizedException
	{
		// Token: 0x0600B83B RID: 47163 RVA: 0x002A4514 File Offset: 0x002A2714
		public UnableToSetMSSRegistryValueException(string registryKey, string exceptionMessage) : base(Strings.UnableToSetMSSRegistryValue(registryKey, exceptionMessage))
		{
			this.registryKey = registryKey;
			this.exceptionMessage = exceptionMessage;
		}

		// Token: 0x0600B83C RID: 47164 RVA: 0x002A4531 File Offset: 0x002A2731
		public UnableToSetMSSRegistryValueException(string registryKey, string exceptionMessage, Exception innerException) : base(Strings.UnableToSetMSSRegistryValue(registryKey, exceptionMessage), innerException)
		{
			this.registryKey = registryKey;
			this.exceptionMessage = exceptionMessage;
		}

		// Token: 0x0600B83D RID: 47165 RVA: 0x002A4550 File Offset: 0x002A2750
		protected UnableToSetMSSRegistryValueException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.registryKey = (string)info.GetValue("registryKey", typeof(string));
			this.exceptionMessage = (string)info.GetValue("exceptionMessage", typeof(string));
		}

		// Token: 0x0600B83E RID: 47166 RVA: 0x002A45A5 File Offset: 0x002A27A5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("registryKey", this.registryKey);
			info.AddValue("exceptionMessage", this.exceptionMessage);
		}

		// Token: 0x17003A14 RID: 14868
		// (get) Token: 0x0600B83F RID: 47167 RVA: 0x002A45D1 File Offset: 0x002A27D1
		public string RegistryKey
		{
			get
			{
				return this.registryKey;
			}
		}

		// Token: 0x17003A15 RID: 14869
		// (get) Token: 0x0600B840 RID: 47168 RVA: 0x002A45D9 File Offset: 0x002A27D9
		public string ExceptionMessage
		{
			get
			{
				return this.exceptionMessage;
			}
		}

		// Token: 0x0400642F RID: 25647
		private readonly string registryKey;

		// Token: 0x04006430 RID: 25648
		private readonly string exceptionMessage;
	}
}
