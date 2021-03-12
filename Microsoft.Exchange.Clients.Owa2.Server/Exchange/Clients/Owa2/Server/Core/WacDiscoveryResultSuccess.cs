using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000055 RID: 85
	public class WacDiscoveryResultSuccess : WacDiscoveryResultBase
	{
		// Token: 0x0600028C RID: 652 RVA: 0x0000996C File Offset: 0x00007B6C
		public WacDiscoveryResultSuccess()
		{
			this.wacViewUrlTemplateMapping = new Dictionary<string, string>(13);
			this.wacEditUrlTemplateMapping = new Dictionary<string, string>(13);
			this.viewOnlyFileTypes = new HashSet<string>
			{
				".odt",
				".ott",
				".fodt",
				".fott",
				".ods",
				".ots",
				".fods",
				".fots",
				".odp",
				".otp",
				".fodp",
				".fotp",
				".odc",
				".otc",
				".fodc",
				".fotc",
				".odi",
				".oti",
				".fodi",
				".foti",
				".odg",
				".otg",
				".fodg",
				".fotg",
				".odf",
				".otf",
				".fodf",
				".fotf",
				".odb",
				".fodb",
				".odm",
				".fodm",
				".oth",
				".foth"
			};
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00009B3E File Offset: 0x00007D3E
		public override string[] WacViewableFileTypes
		{
			get
			{
				if (!this.isInitialized)
				{
					throw new InvalidOperationException("This operation should not be invoked when the object has not been completely initialized yet.");
				}
				return this.wacSupportedFileTypes;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00009B59 File Offset: 0x00007D59
		public override string[] WacEditableFileTypes
		{
			get
			{
				if (!this.isInitialized)
				{
					throw new InvalidOperationException("This operation should not be invoked when the object has not been completely initialized yet.");
				}
				return this.wacEditableFileTypes;
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00009B74 File Offset: 0x00007D74
		public override string GetWacViewableFileTypesDisplayText()
		{
			StringBuilder stringBuilder = new StringBuilder(40);
			foreach (string text in this.wacViewUrlTemplateMapping.Keys)
			{
				stringBuilder.Append(text);
				stringBuilder.Append("-> ");
				stringBuilder.Append(this.wacViewUrlTemplateMapping[text]);
				stringBuilder.Append(";");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00009C08 File Offset: 0x00007E08
		public override void AddViewMapping(string fileExtension, string path)
		{
			if (this.isInitialized)
			{
				throw new InvalidOperationException("This operation should not be invoked once the object has been marked as completely initialized");
			}
			fileExtension = fileExtension.ToLowerInvariant();
			this.wacViewUrlTemplateMapping[fileExtension] = path;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00009C32 File Offset: 0x00007E32
		public override void AddEditMapping(string fileExtension, string path)
		{
			if (this.isInitialized)
			{
				throw new InvalidOperationException("This operation should not be invoked once the object has been marked as completely initialized");
			}
			fileExtension = fileExtension.ToLowerInvariant();
			if (this.viewOnlyFileTypes.Contains(fileExtension))
			{
				return;
			}
			this.wacEditUrlTemplateMapping[fileExtension] = path;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00009C6B File Offset: 0x00007E6B
		public override bool TryGetViewUrlForFileExtension(string extension, string cultureName, out string url)
		{
			return WacDiscoveryResultSuccess.TryGetUrlForFileExtension(this.wacViewUrlTemplateMapping, "view", extension, cultureName, out url);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00009C80 File Offset: 0x00007E80
		public override bool TryGetEditUrlForFileExtension(string extension, string cultureName, out string url)
		{
			return WacDiscoveryResultSuccess.TryGetUrlForFileExtension(this.wacEditUrlTemplateMapping, "edit", extension, cultureName, out url);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00009C95 File Offset: 0x00007E95
		public override void MarkInitializationComplete()
		{
			this.CreateSupportedItemsArrayFromMapping();
			this.CreateEditableItemsArrayFromMapping();
			this.isInitialized = true;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00009CAC File Offset: 0x00007EAC
		private static bool TryGetUrlForFileExtension(Dictionary<string, string> mapping, string verb, string extension, string cultureName, out string url)
		{
			if (string.IsNullOrEmpty(extension))
			{
				throw new ArgumentException("extension");
			}
			if (mapping == null)
			{
				throw new ArgumentException("mapping");
			}
			extension = extension.ToLowerInvariant();
			if (!mapping.ContainsKey(extension))
			{
				url = null;
				return false;
			}
			string text = mapping[extension];
			url = text.Replace("<ui=UI_LLCC&>", string.Format("ui={0}&", cultureName));
			url = url.Replace("<rs=DC_LLCC&>", string.Format("rs={0}&", cultureName));
			url = url.Replace("<showpagestats=PERFSTATS&>", string.Empty);
			if (!WacConfiguration.Instance.UseHttpsForWacUrl)
			{
				url = url.Replace("https", "http");
			}
			return true;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00009D64 File Offset: 0x00007F64
		private void CreateSupportedItemsArrayFromMapping()
		{
			int count = this.wacViewUrlTemplateMapping.Keys.Count;
			this.wacSupportedFileTypes = new string[count];
			int num = 0;
			foreach (string text in this.wacViewUrlTemplateMapping.Keys)
			{
				this.wacSupportedFileTypes[num++] = text;
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00009DE4 File Offset: 0x00007FE4
		private void CreateEditableItemsArrayFromMapping()
		{
			int count = this.wacEditUrlTemplateMapping.Keys.Count;
			this.wacEditableFileTypes = new string[count];
			int num = 0;
			foreach (string text in this.wacEditUrlTemplateMapping.Keys)
			{
				this.wacEditableFileTypes[num++] = text;
			}
		}

		// Token: 0x0400012F RID: 303
		private Dictionary<string, string> wacViewUrlTemplateMapping;

		// Token: 0x04000130 RID: 304
		private Dictionary<string, string> wacEditUrlTemplateMapping;

		// Token: 0x04000131 RID: 305
		private string[] wacSupportedFileTypes;

		// Token: 0x04000132 RID: 306
		private string[] wacEditableFileTypes;

		// Token: 0x04000133 RID: 307
		private HashSet<string> viewOnlyFileTypes;

		// Token: 0x04000134 RID: 308
		private bool isInitialized;
	}
}
