using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000DF0 RID: 3568
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToAddE12DStoExSPermissionsException : LocalizedException
	{
		// Token: 0x0600A4A4 RID: 42148 RVA: 0x0028498C File Offset: 0x00282B8C
		public UnableToAddE12DStoExSPermissionsException(string dom, string e12ds, string exs, string exsShort, string localdomdc, string rootdomdc) : base(Strings.UnableToAddE12DStoExSPermissionsException(dom, e12ds, exs, exsShort, localdomdc, rootdomdc))
		{
			this.dom = dom;
			this.e12ds = e12ds;
			this.exs = exs;
			this.exsShort = exsShort;
			this.localdomdc = localdomdc;
			this.rootdomdc = rootdomdc;
		}

		// Token: 0x0600A4A5 RID: 42149 RVA: 0x002849DC File Offset: 0x00282BDC
		public UnableToAddE12DStoExSPermissionsException(string dom, string e12ds, string exs, string exsShort, string localdomdc, string rootdomdc, Exception innerException) : base(Strings.UnableToAddE12DStoExSPermissionsException(dom, e12ds, exs, exsShort, localdomdc, rootdomdc), innerException)
		{
			this.dom = dom;
			this.e12ds = e12ds;
			this.exs = exs;
			this.exsShort = exsShort;
			this.localdomdc = localdomdc;
			this.rootdomdc = rootdomdc;
		}

		// Token: 0x0600A4A6 RID: 42150 RVA: 0x00284A2C File Offset: 0x00282C2C
		protected UnableToAddE12DStoExSPermissionsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dom = (string)info.GetValue("dom", typeof(string));
			this.e12ds = (string)info.GetValue("e12ds", typeof(string));
			this.exs = (string)info.GetValue("exs", typeof(string));
			this.exsShort = (string)info.GetValue("exsShort", typeof(string));
			this.localdomdc = (string)info.GetValue("localdomdc", typeof(string));
			this.rootdomdc = (string)info.GetValue("rootdomdc", typeof(string));
		}

		// Token: 0x0600A4A7 RID: 42151 RVA: 0x00284B04 File Offset: 0x00282D04
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dom", this.dom);
			info.AddValue("e12ds", this.e12ds);
			info.AddValue("exs", this.exs);
			info.AddValue("exsShort", this.exsShort);
			info.AddValue("localdomdc", this.localdomdc);
			info.AddValue("rootdomdc", this.rootdomdc);
		}

		// Token: 0x17003601 RID: 13825
		// (get) Token: 0x0600A4A8 RID: 42152 RVA: 0x00284B7F File Offset: 0x00282D7F
		public string Dom
		{
			get
			{
				return this.dom;
			}
		}

		// Token: 0x17003602 RID: 13826
		// (get) Token: 0x0600A4A9 RID: 42153 RVA: 0x00284B87 File Offset: 0x00282D87
		public string E12ds
		{
			get
			{
				return this.e12ds;
			}
		}

		// Token: 0x17003603 RID: 13827
		// (get) Token: 0x0600A4AA RID: 42154 RVA: 0x00284B8F File Offset: 0x00282D8F
		public string Exs
		{
			get
			{
				return this.exs;
			}
		}

		// Token: 0x17003604 RID: 13828
		// (get) Token: 0x0600A4AB RID: 42155 RVA: 0x00284B97 File Offset: 0x00282D97
		public string ExsShort
		{
			get
			{
				return this.exsShort;
			}
		}

		// Token: 0x17003605 RID: 13829
		// (get) Token: 0x0600A4AC RID: 42156 RVA: 0x00284B9F File Offset: 0x00282D9F
		public string Localdomdc
		{
			get
			{
				return this.localdomdc;
			}
		}

		// Token: 0x17003606 RID: 13830
		// (get) Token: 0x0600A4AD RID: 42157 RVA: 0x00284BA7 File Offset: 0x00282DA7
		public string Rootdomdc
		{
			get
			{
				return this.rootdomdc;
			}
		}

		// Token: 0x04005F67 RID: 24423
		private readonly string dom;

		// Token: 0x04005F68 RID: 24424
		private readonly string e12ds;

		// Token: 0x04005F69 RID: 24425
		private readonly string exs;

		// Token: 0x04005F6A RID: 24426
		private readonly string exsShort;

		// Token: 0x04005F6B RID: 24427
		private readonly string localdomdc;

		// Token: 0x04005F6C RID: 24428
		private readonly string rootdomdc;
	}
}
