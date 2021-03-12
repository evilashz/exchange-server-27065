using System;
using System.Data;
using System.Text;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data.Directory;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000135 RID: 309
	public abstract class ProcedureBuilder : ICommandBuilder
	{
		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0002BA90 File Offset: 0x00029C90
		// (set) Token: 0x06000C32 RID: 3122 RVA: 0x0002BA98 File Offset: 0x00029C98
		public string ResolveProperty { get; set; }

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x0002BAA1 File Offset: 0x00029CA1
		// (set) Token: 0x06000C34 RID: 3124 RVA: 0x0002BAA9 File Offset: 0x00029CA9
		public ExchangeCommandBuilderSearch SearchType { get; set; }

		// Token: 0x06000C35 RID: 3125 RVA: 0x0002BAB2 File Offset: 0x00029CB2
		public string BuildCommand(string commandText, string searchText, object[] pipeline, DataRow row)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0002BAB9 File Offset: 0x00029CB9
		public string BuildCommandWithScope(string commandText, string searchText, object[] pipeline, DataRow row, object scope)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x0002BAC0 File Offset: 0x00029CC0
		// (set) Token: 0x06000C38 RID: 3128 RVA: 0x0002BAC8 File Offset: 0x00029CC8
		public string NamePropertyFilter { get; set; }

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0002BAD1 File Offset: 0x00029CD1
		// (set) Token: 0x06000C3A RID: 3130 RVA: 0x0002BAD9 File Offset: 0x00029CD9
		public bool UseFilterToResolveNonId { get; set; }

		// Token: 0x06000C3B RID: 3131 RVA: 0x0002BAE2 File Offset: 0x00029CE2
		internal MonadCommand BuildProcedure(string commandText, string searchText, object[] pipeline, DataRow row)
		{
			return this.BuildProcedureWithScope(commandText, searchText, pipeline, row, null);
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0002BAF0 File Offset: 0x00029CF0
		internal MonadCommand BuildProcedureWithScope(string commandText, string searchText, object[] pipeline, DataRow row, object scope)
		{
			MonadCommand monadCommand = this.InnerBuildProcedureCore(commandText, row);
			if (!string.IsNullOrEmpty(searchText))
			{
				switch (this.SearchType)
				{
				case 0:
					monadCommand.Parameters.AddWithValue("ANR", searchText);
					break;
				case 1:
					monadCommand.Parameters.AddWithValue("Identity", string.Format("*{0}*", searchText));
					break;
				}
			}
			if (!string.IsNullOrEmpty(scope as string))
			{
				monadCommand.Parameters.AddWithValue("OrganizationalUnit", scope);
			}
			if (this.UseFilterToResolveNonId)
			{
				string pipelineFilterStringNotResolveIdentity = this.GetPipelineFilterStringNotResolveIdentity(pipeline);
				if (!string.IsNullOrEmpty(pipelineFilterStringNotResolveIdentity))
				{
					string value = pipelineFilterStringNotResolveIdentity;
					if (monadCommand.Parameters.Contains("Filter"))
					{
						value = string.Format("({0}) -and ({1})", pipelineFilterStringNotResolveIdentity, monadCommand.Parameters["Filter"].Value);
						monadCommand.Parameters["Filter"].Value = value;
					}
					else
					{
						monadCommand.Parameters.AddWithValue("Filter", value);
					}
				}
			}
			return monadCommand;
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0002BBF0 File Offset: 0x00029DF0
		private string GetPipelineFilterStringNotResolveIdentity(object[] pipeline)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (pipeline != null && pipeline.Length > 0)
			{
				for (int i = 0; i < pipeline.Length; i++)
				{
					ADObjectId adobjectId = pipeline[i] as ADObjectId;
					string item = string.Format((pipeline.Length - 1 == i) ? "{0} -eq '{1}'" : "{0} -eq '{1}' -or ", this.ResolveProperty, (adobjectId != null) ? adobjectId.DistinguishedName.ToQuotationEscapedString() : pipeline[i].ToQuotationEscapedString());
					stringBuilder.Append(item.ToQuotationEscapedString());
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000C3E RID: 3134
		internal abstract MonadCommand InnerBuildProcedureCore(string commandText, DataRow row);
	}
}
