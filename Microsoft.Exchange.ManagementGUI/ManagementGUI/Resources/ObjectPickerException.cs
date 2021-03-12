using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.ManagementGUI.Resources
{
	// Token: 0x02000013 RID: 19
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ObjectPickerException : LocalizedException
	{
		// Token: 0x060010AB RID: 4267 RVA: 0x00036F18 File Offset: 0x00035118
		public ObjectPickerException(string backgroundThreadMessage) : base(Strings.ObjectPickerError(backgroundThreadMessage))
		{
			this.backgroundThreadMessage = backgroundThreadMessage;
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x00036F2D File Offset: 0x0003512D
		public ObjectPickerException(string backgroundThreadMessage, Exception innerException) : base(Strings.ObjectPickerError(backgroundThreadMessage), innerException)
		{
			this.backgroundThreadMessage = backgroundThreadMessage;
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x00036F43 File Offset: 0x00035143
		protected ObjectPickerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.backgroundThreadMessage = (string)info.GetValue("backgroundThreadMessage", typeof(string));
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00036F6D File Offset: 0x0003516D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("backgroundThreadMessage", this.backgroundThreadMessage);
		}

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x060010AF RID: 4271 RVA: 0x00036F88 File Offset: 0x00035188
		public string BackgroundThreadMessage
		{
			get
			{
				return this.backgroundThreadMessage;
			}
		}

		// Token: 0x0400106F RID: 4207
		private readonly string backgroundThreadMessage;
	}
}
