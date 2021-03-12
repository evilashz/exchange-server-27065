using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000FCC RID: 4044
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class XmlDeserializationException : LocalizedException
	{
		// Token: 0x0600ADD0 RID: 44496 RVA: 0x002923BA File Offset: 0x002905BA
		public XmlDeserializationException(string filename, string error, string addlInfo) : base(Strings.XmlDeserializationException(filename, error, addlInfo))
		{
			this.filename = filename;
			this.error = error;
			this.addlInfo = addlInfo;
		}

		// Token: 0x0600ADD1 RID: 44497 RVA: 0x002923DF File Offset: 0x002905DF
		public XmlDeserializationException(string filename, string error, string addlInfo, Exception innerException) : base(Strings.XmlDeserializationException(filename, error, addlInfo), innerException)
		{
			this.filename = filename;
			this.error = error;
			this.addlInfo = addlInfo;
		}

		// Token: 0x0600ADD2 RID: 44498 RVA: 0x00292408 File Offset: 0x00290608
		protected XmlDeserializationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.filename = (string)info.GetValue("filename", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
			this.addlInfo = (string)info.GetValue("addlInfo", typeof(string));
		}

		// Token: 0x0600ADD3 RID: 44499 RVA: 0x0029247D File Offset: 0x0029067D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("filename", this.filename);
			info.AddValue("error", this.error);
			info.AddValue("addlInfo", this.addlInfo);
		}

		// Token: 0x170037BD RID: 14269
		// (get) Token: 0x0600ADD4 RID: 44500 RVA: 0x002924BA File Offset: 0x002906BA
		public string Filename
		{
			get
			{
				return this.filename;
			}
		}

		// Token: 0x170037BE RID: 14270
		// (get) Token: 0x0600ADD5 RID: 44501 RVA: 0x002924C2 File Offset: 0x002906C2
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x170037BF RID: 14271
		// (get) Token: 0x0600ADD6 RID: 44502 RVA: 0x002924CA File Offset: 0x002906CA
		public string AddlInfo
		{
			get
			{
				return this.addlInfo;
			}
		}

		// Token: 0x04006123 RID: 24867
		private readonly string filename;

		// Token: 0x04006124 RID: 24868
		private readonly string error;

		// Token: 0x04006125 RID: 24869
		private readonly string addlInfo;
	}
}
