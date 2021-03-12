using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200019E RID: 414
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnknownConnectionSettingsTypeException : MigrationPermanentException
	{
		// Token: 0x06001767 RID: 5991 RVA: 0x000709E5 File Offset: 0x0006EBE5
		public UnknownConnectionSettingsTypeException(string root) : base(Strings.ErrorUnknownConnectionSettingsType(root))
		{
			this.root = root;
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x000709FA File Offset: 0x0006EBFA
		public UnknownConnectionSettingsTypeException(string root, Exception innerException) : base(Strings.ErrorUnknownConnectionSettingsType(root), innerException)
		{
			this.root = root;
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x00070A10 File Offset: 0x0006EC10
		protected UnknownConnectionSettingsTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.root = (string)info.GetValue("root", typeof(string));
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x00070A3A File Offset: 0x0006EC3A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("root", this.root);
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x0600176B RID: 5995 RVA: 0x00070A55 File Offset: 0x0006EC55
		public string Root
		{
			get
			{
				return this.root;
			}
		}

		// Token: 0x04000B17 RID: 2839
		private readonly string root;
	}
}
