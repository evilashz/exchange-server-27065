using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.ManagementConsole;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020000D6 RID: 214
	[Serializable]
	public class NodeProfile
	{
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x000196DB File Offset: 0x000178DB
		// (set) Token: 0x06000787 RID: 1927 RVA: 0x000196E3 File Offset: 0x000178E3
		[DefaultValue(null)]
		[TypeConverter(typeof(OrganizationTypesConverter))]
		public OrganizationType[] OrganizationTypes { get; set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x000196EC File Offset: 0x000178EC
		// (set) Token: 0x06000789 RID: 1929 RVA: 0x000196F4 File Offset: 0x000178F4
		public ResultPaneProfile ResultPaneProfile { get; set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x000196FD File Offset: 0x000178FD
		// (set) Token: 0x0600078B RID: 1931 RVA: 0x00019705 File Offset: 0x00017905
		[TypeConverter(typeof(DDIObjectTypeConverter))]
		public Type Type { get; set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x0001970E File Offset: 0x0001790E
		// (set) Token: 0x0600078D RID: 1933 RVA: 0x00019716 File Offset: 0x00017916
		public string Name { get; set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x0001971F File Offset: 0x0001791F
		// (set) Token: 0x0600078F RID: 1935 RVA: 0x00019727 File Offset: 0x00017927
		public NodeProfileList NodeProfiles
		{
			get
			{
				return this.nodeProfiles;
			}
			set
			{
				this.nodeProfiles = value;
			}
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00019730 File Offset: 0x00017930
		public ScopeNode[] GetScopeNodes()
		{
			List<ScopeNode> list = new List<ScopeNode>();
			foreach (NodeProfile nodeProfile in this.nodeProfiles)
			{
				if (WinformsHelper.IsCurrentOrganizationAllowed(nodeProfile.OrganizationTypes) && nodeProfile.Type != null)
				{
					bool flag = false;
					ExchangeScopeNode exchangeScopeNode = (ExchangeScopeNode)nodeProfile.Type.GetConstructor(new Type[0]).Invoke(new object[0]);
					ScopeNode[] scopeNodes = nodeProfile.GetScopeNodes();
					if (scopeNodes.Length > 0)
					{
						flag = true;
						exchangeScopeNode.Children.AddRange(scopeNodes);
					}
					if (nodeProfile.ResultPaneProfile != null && nodeProfile.ResultPaneProfile.HasPermission())
					{
						exchangeScopeNode.ViewDescriptions.Add(ExchangeFormView.CreateViewDescription(nodeProfile.ResultPaneProfile.Type));
						flag = true;
					}
					if (scopeNodes.Length > 0 && exchangeScopeNode.ViewDescriptions.Count == 0)
					{
						exchangeScopeNode.ViewDescriptions.Add(ExchangeFormView.CreateViewDescription(typeof(RbacPermissionLockResultPane)));
					}
					if (flag)
					{
						list.Add(exchangeScopeNode);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00019860 File Offset: 0x00017A60
		public static bool CanAddAtomResultPane(Type atomResultPaneType)
		{
			FieldInfo field = atomResultPaneType.GetField("SchemaName");
			if (null != field)
			{
				string profileName = (string)field.GetValue(null);
				return NodeProfile.dataProfileLoader.GetProfile(profileName).HasPermission();
			}
			return true;
		}

		// Token: 0x04000390 RID: 912
		private static readonly ObjectPickerProfileLoader dataProfileLoader = new ObjectPickerProfileLoader(1);

		// Token: 0x04000391 RID: 913
		private NodeProfileList nodeProfiles = new NodeProfileList();
	}
}
