using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020003BE RID: 958
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CorruptLogDetectedException : TransientException
	{
		// Token: 0x0600280F RID: 10255 RVA: 0x000B7199 File Offset: 0x000B5399
		public CorruptLogDetectedException(string filename, string errorText) : base(ReplayStrings.CorruptLogDetectedError(filename, errorText))
		{
			this.filename = filename;
			this.errorText = errorText;
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x000B71B6 File Offset: 0x000B53B6
		public CorruptLogDetectedException(string filename, string errorText, Exception innerException) : base(ReplayStrings.CorruptLogDetectedError(filename, errorText), innerException)
		{
			this.filename = filename;
			this.errorText = errorText;
		}

		// Token: 0x06002811 RID: 10257 RVA: 0x000B71D4 File Offset: 0x000B53D4
		protected CorruptLogDetectedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.filename = (string)info.GetValue("filename", typeof(string));
			this.errorText = (string)info.GetValue("errorText", typeof(string));
		}

		// Token: 0x06002812 RID: 10258 RVA: 0x000B7229 File Offset: 0x000B5429
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("filename", this.filename);
			info.AddValue("errorText", this.errorText);
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06002813 RID: 10259 RVA: 0x000B7255 File Offset: 0x000B5455
		public string Filename
		{
			get
			{
				return this.filename;
			}
		}

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06002814 RID: 10260 RVA: 0x000B725D File Offset: 0x000B545D
		public string ErrorText
		{
			get
			{
				return this.errorText;
			}
		}

		// Token: 0x040013BE RID: 5054
		private readonly string filename;

		// Token: 0x040013BF RID: 5055
		private readonly string errorText;
	}
}
