using System;
using System.Windows.Markup;
using System.Xaml;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000144 RID: 324
	[ContentProperty("Name")]
	[MarkupExtensionReturnType(typeof(Activity))]
	public class ActivityReferenceExtension : MarkupExtension
	{
		// Token: 0x06002135 RID: 8501 RVA: 0x00064357 File Offset: 0x00062557
		public ActivityReferenceExtension()
		{
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x0006435F File Offset: 0x0006255F
		public ActivityReferenceExtension(string name)
		{
			this.Name = name;
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x00064370 File Offset: 0x00062570
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			IRootObjectProvider rootObjectProvider = (IRootObjectProvider)serviceProvider.GetService(typeof(IRootObjectProvider));
			object obj = ((Service)rootObjectProvider.RootObject).Resources[this.Name];
			if (obj is Activity)
			{
				return ((Activity)obj).Clone();
			}
			throw new InvalidOperationException("ActivityReference can't reference to a value which is not an Activity");
		}

		// Token: 0x17001A61 RID: 6753
		// (get) Token: 0x06002138 RID: 8504 RVA: 0x000643CD File Offset: 0x000625CD
		// (set) Token: 0x06002139 RID: 8505 RVA: 0x000643D5 File Offset: 0x000625D5
		public string Name { get; set; }
	}
}
