using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.ControlPanel;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200033A RID: 826
	public static class OrganizationalUnitPickerService
	{
		// Token: 0x06002F37 RID: 12087 RVA: 0x000900C0 File Offset: 0x0008E2C0
		public static void GetListPostAction(DataRow inputRow, DataTable dataTable, DataObjectStore store)
		{
			dataTable.BeginLoadData();
			DataView defaultView = dataTable.DefaultView;
			defaultView.Sort = "Identity asc";
			OrganizationalUnitPickerService.OUElement ouelement = OrganizationalUnitPickerService.BuildOUTree(defaultView.ToTable());
			dataTable.Rows.Clear();
			DataRow dataRow = dataTable.NewRow();
			dataRow["ID"] = ouelement.ID;
			dataRow["Name"] = ouelement.Name;
			dataRow["CanNewSubNode"] = ouelement.CanNewSubNode;
			dataRow["Children"] = ouelement.Children;
			dataTable.Rows.Add(dataRow);
			dataTable.EndLoadData();
		}

		// Token: 0x06002F38 RID: 12088 RVA: 0x00090160 File Offset: 0x0008E360
		private static OrganizationalUnitPickerService.OUElement BuildOUTree(DataTable dataTable)
		{
			Dictionary<string, OrganizationalUnitPickerService.OUElement> dictionary = new Dictionary<string, OrganizationalUnitPickerService.OUElement>();
			OrganizationalUnitPickerService.OUElement ouelement = new OrganizationalUnitPickerService.OUElement();
			ouelement.ID = (ouelement.Name = "root");
			ouelement.CanNewSubNode = false;
			dictionary.Add(ouelement.Name, ouelement);
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				ADObjectId adobjectId = (ADObjectId)dataRow["Identity"];
				string text = adobjectId.ToString();
				int num = OrganizationalUnitPickerService.LastSplitCharPosition(text);
				string key = (num == -1) ? ouelement.Name : text.Substring(0, num);
				if (dictionary.ContainsKey(key))
				{
					OrganizationalUnitPickerService.OUElement ouelement2 = new OrganizationalUnitPickerService.OUElement();
					ouelement2.ID = adobjectId.ObjectGuid.ToString();
					ouelement2.CanNewSubNode = false;
					ouelement2.Name = text.Substring(num + 1);
					dictionary[key].Children.Add(ouelement2);
					dictionary.Add(text, ouelement2);
				}
			}
			return ouelement;
		}

		// Token: 0x06002F39 RID: 12089 RVA: 0x00090298 File Offset: 0x0008E498
		private static int LastSplitCharPosition(string canonicalName)
		{
			int result = -1;
			for (int i = 0; i < canonicalName.Length; i++)
			{
				char c = canonicalName[i];
				if (c == '\\')
				{
					i++;
				}
				else if (c == '/')
				{
					result = i;
				}
			}
			return result;
		}

		// Token: 0x0200033C RID: 828
		[DataContract]
		public class OUElement : NodeInfo
		{
			// Token: 0x17001EED RID: 7917
			// (get) Token: 0x06002F41 RID: 12097 RVA: 0x00090310 File Offset: 0x0008E510
			// (set) Token: 0x06002F42 RID: 12098 RVA: 0x00090318 File Offset: 0x0008E518
			[DataMember]
			public List<object> Children
			{
				get
				{
					return this.children;
				}
				private set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x06002F43 RID: 12099 RVA: 0x0009031F File Offset: 0x0008E51F
			public override int GetHashCode()
			{
				return base.Name.GetHashCode() + base.ID.GetHashCode() + this.Children.GetHashCode();
			}

			// Token: 0x06002F44 RID: 12100 RVA: 0x00090344 File Offset: 0x0008E544
			public override bool Equals(object obj)
			{
				OrganizationalUnitPickerService.OUElement ouelement = obj as OrganizationalUnitPickerService.OUElement;
				return ouelement != null && (string.Compare(base.Name, ouelement.Name) == 0 && string.Compare(base.ID, ouelement.ID) == 0 && base.CanNewSubNode == ouelement.CanNewSubNode) && this.Children.Count == ouelement.Children.Count;
			}

			// Token: 0x040022FF RID: 8959
			private List<object> children = new List<object>();
		}
	}
}
