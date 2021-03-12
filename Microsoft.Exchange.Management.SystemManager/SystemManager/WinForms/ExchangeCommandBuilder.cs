using System;
using System.Data;
using System.IO;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000130 RID: 304
	public class ExchangeCommandBuilder : ICommandBuilder
	{
		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x0002B633 File Offset: 0x00029833
		// (set) Token: 0x06000C17 RID: 3095 RVA: 0x0002B63B File Offset: 0x0002983B
		public IExchangeScopeBuilder ScopeBuilder
		{
			get
			{
				return this.scopeBuilder;
			}
			set
			{
				this.scopeBuilder = value;
			}
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0002B644 File Offset: 0x00029844
		public ExchangeCommandBuilder() : this(null)
		{
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0002B64D File Offset: 0x0002984D
		public ExchangeCommandBuilder(IExchangeCommandFilterBuilder filterBuilder)
		{
			this.filterBuilder = filterBuilder;
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x0002B66E File Offset: 0x0002986E
		public string BuildCommandWithScope(string commandText, string searchText, object[] pipeline, DataRow row, object scope)
		{
			return this.BuildCommandCore(commandText, searchText, pipeline, row, this.scopeBuilder.BuildScope(scope));
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x0002B688 File Offset: 0x00029888
		public string BuildCommand(string commandText, string searchText, object[] pipeline, DataRow row)
		{
			return this.BuildCommandCore(commandText, searchText, pipeline, row, null);
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x0002B698 File Offset: 0x00029898
		private string BuildCommandCore(string commandText, string searchText, object[] pipeline, DataRow row, string scopeSetting)
		{
			bool flag = false;
			if (string.IsNullOrEmpty(commandText))
			{
				throw new ArgumentNullException("Cannot build a command without name.");
			}
			string text = null;
			string value = null;
			string text2 = null;
			if (this.FilterBuilder != null)
			{
				this.FilterBuilder.BuildFilter(out text, out value, out text2, row);
			}
			StringBuilder stringBuilder = new StringBuilder();
			string text3 = this.GetPipelineStringWhenResolveIdentity(pipeline);
			if (!string.IsNullOrEmpty(text3))
			{
				stringBuilder.AppendFormat("{0}", text3);
			}
			stringBuilder.Append(commandText);
			if (!string.IsNullOrEmpty(searchText))
			{
				switch (this.SearchType)
				{
				case 0:
					stringBuilder.AppendFormat(" -ANR '{0}'", searchText.ToQuotationEscapedString());
					break;
				case 1:
					stringBuilder.AppendFormat(" -Identity '*{0}*'", searchText.ToQuotationEscapedString());
					break;
				}
			}
			if (!string.IsNullOrEmpty(scopeSetting))
			{
				stringBuilder.Append(" " + scopeSetting);
			}
			if (!string.IsNullOrEmpty(text))
			{
				stringBuilder.AppendFormat(" {0}", text);
			}
			if (this.UseFilterToResolveNonId)
			{
				text3 = this.GetPipelineFilterStringNotResolveIdentity(pipeline);
				if (!string.IsNullOrEmpty(text3))
				{
					stringBuilder.AppendFormat(" {0}", text3);
				}
			}
			if (!string.IsNullOrEmpty(value))
			{
				stringBuilder.Append(value);
				flag = true;
			}
			if (this.searchType == 2 && !string.IsNullOrEmpty(this.NamePropertyFilter) && !string.IsNullOrEmpty(searchText))
			{
				stringBuilder.AppendFormat(" | Filter-PropertyStringContains -Property '{0}' -SearchText '{1}'", this.NamePropertyFilter.ToQuotationEscapedString(), searchText.ToQuotationEscapedString());
				flag = true;
			}
			if (!this.resolveForIdentity() && !this.UseFilterToResolveNonId && pipeline != null && pipeline.Length > 0)
			{
				stringBuilder.AppendFormat(" | Filter-PropertyInObjects -ResolveProperty '{0}' -inputObjects {1}", this.ResolveProperty.ToQuotationEscapedString(), this.GetPipelineWhereStringNotResolveIdentity(pipeline));
				flag = true;
			}
			if (flag && !WinformsHelper.IsRemoteEnabled())
			{
				string str = Path.Combine(ConfigurationContext.Setup.RemoteScriptPath, "ConsoleInitialize.ps1");
				stringBuilder.Insert(0, ". '" + str + "';");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x0002B878 File Offset: 0x00029A78
		// (set) Token: 0x06000C1E RID: 3102 RVA: 0x0002B880 File Offset: 0x00029A80
		public ExchangeCommandBuilderSearch SearchType
		{
			get
			{
				return this.searchType;
			}
			set
			{
				this.searchType = value;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x0002B889 File Offset: 0x00029A89
		// (set) Token: 0x06000C20 RID: 3104 RVA: 0x0002B891 File Offset: 0x00029A91
		public string ResolveProperty
		{
			get
			{
				return this.resolveProperty;
			}
			set
			{
				this.resolveProperty = value;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x0002B89A File Offset: 0x00029A9A
		// (set) Token: 0x06000C22 RID: 3106 RVA: 0x0002B8A2 File Offset: 0x00029AA2
		public bool UseFilterToResolveNonId
		{
			get
			{
				return this.useFilterToResolveNonId;
			}
			set
			{
				this.useFilterToResolveNonId = value;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x0002B8AB File Offset: 0x00029AAB
		// (set) Token: 0x06000C24 RID: 3108 RVA: 0x0002B8B3 File Offset: 0x00029AB3
		public string NamePropertyFilter { get; set; }

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x0002B8BC File Offset: 0x00029ABC
		// (set) Token: 0x06000C26 RID: 3110 RVA: 0x0002B8C4 File Offset: 0x00029AC4
		public IExchangeCommandFilterBuilder FilterBuilder
		{
			get
			{
				return this.filterBuilder;
			}
			set
			{
				this.filterBuilder = value;
			}
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0002B8CD File Offset: 0x00029ACD
		internal bool resolveForIdentity()
		{
			return string.IsNullOrEmpty(this.ResolveProperty) || string.Equals("Id", this.ResolveProperty, StringComparison.InvariantCultureIgnoreCase) || string.Equals("Identity", this.ResolveProperty, StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x0002B904 File Offset: 0x00029B04
		private string GetPipelineStringWhenResolveIdentity(object[] pipeline)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (pipeline != null && pipeline.Length > 0 && this.resolveForIdentity())
			{
				for (int i = 0; i < pipeline.Length; i++)
				{
					stringBuilder.AppendFormat((pipeline.Length - 1 != i) ? "'{0}'," : "'{0}' | ", pipeline[i].ToSustainedString());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0002B960 File Offset: 0x00029B60
		private string GetPipelineWhereStringNotResolveIdentity(object[] pipeline)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (pipeline != null && pipeline.Length > 0)
			{
				for (int i = 0; i < pipeline.Length; i++)
				{
					stringBuilder.AppendFormat((pipeline.Length - 1 != i) ? "'{0}'," : "'{0}'", pipeline[i].ToQuotationEscapedString());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0002B9B4 File Offset: 0x00029BB4
		private string GetPipelineFilterStringNotResolveIdentity(object[] pipeline)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (pipeline != null && pipeline.Length > 0 && !this.resolveForIdentity())
			{
				stringBuilder.Append("-Filter '");
				for (int i = 0; i < pipeline.Length; i++)
				{
					ADObjectId adobjectId = pipeline[i] as ADObjectId;
					string item = string.Format((pipeline.Length - 1 == i) ? "{0} -eq '{1}'" : "{0} -eq '{1}' -or ", this.ResolveProperty, (adobjectId != null) ? adobjectId.DistinguishedName.ToQuotationEscapedString() : pipeline[i].ToQuotationEscapedString());
					stringBuilder.Append(item.ToQuotationEscapedString());
				}
				stringBuilder.Append("'");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040004F6 RID: 1270
		private ExchangeCommandBuilderSearch searchType = 1;

		// Token: 0x040004F7 RID: 1271
		private string resolveProperty;

		// Token: 0x040004F8 RID: 1272
		private IExchangeCommandFilterBuilder filterBuilder;

		// Token: 0x040004F9 RID: 1273
		private IExchangeScopeBuilder scopeBuilder = new ExchangeOUScopeBuilder();

		// Token: 0x040004FA RID: 1274
		private bool useFilterToResolveNonId;
	}
}
