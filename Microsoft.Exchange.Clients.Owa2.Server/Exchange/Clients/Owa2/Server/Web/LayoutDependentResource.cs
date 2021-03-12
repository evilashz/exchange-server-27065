using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Clients.Common;

namespace Microsoft.Exchange.Clients.Owa2.Server.Web
{
	// Token: 0x0200048F RID: 1167
	[DataContract]
	public class LayoutDependentResource
	{
		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x060027BF RID: 10175 RVA: 0x00092D8F File Offset: 0x00090F8F
		// (set) Token: 0x060027BE RID: 10174 RVA: 0x00092D71 File Offset: 0x00090F71
		[DataMember(Name = "layout", EmitDefaultValue = false)]
		public string LayoutString
		{
			get
			{
				if (this.Layout != ResourceLayout.Any)
				{
					return this.Layout.ToString();
				}
				return null;
			}
			set
			{
				this.Layout = (ResourceLayout)Enum.Parse(typeof(ResourceLayout), value, true);
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x060027C0 RID: 10176 RVA: 0x00092DAB File Offset: 0x00090FAB
		// (set) Token: 0x060027C1 RID: 10177 RVA: 0x00092DB3 File Offset: 0x00090FB3
		public ResourceLayout Layout { get; set; }

		// Token: 0x060027C2 RID: 10178 RVA: 0x00092DBC File Offset: 0x00090FBC
		public bool IsForLayout(LayoutType layout)
		{
			return this.Layout == ResourceLayout.Any || (this.Layout == ResourceLayout.Mouse && layout == LayoutType.Mouse) || (this.Layout == ResourceLayout.TNarrow && layout == LayoutType.TouchNarrow) || (this.Layout == ResourceLayout.TWide && layout == LayoutType.TouchWide);
		}

		// Token: 0x060027C3 RID: 10179 RVA: 0x00092DF4 File Offset: 0x00090FF4
		public override bool Equals(object obj)
		{
			LayoutDependentResource layoutDependentResource = obj as LayoutDependentResource;
			return layoutDependentResource != null && this.Layout == layoutDependentResource.Layout;
		}

		// Token: 0x060027C4 RID: 10180 RVA: 0x00092E1B File Offset: 0x0009101B
		public override int GetHashCode()
		{
			return this.Layout.GetHashCode();
		}
	}
}
