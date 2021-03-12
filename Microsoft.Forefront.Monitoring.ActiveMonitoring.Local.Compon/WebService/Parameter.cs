using System;
using System.Xml;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.WebService
{
	// Token: 0x02000288 RID: 648
	public class Parameter
	{
		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001540 RID: 5440 RVA: 0x00042052 File Offset: 0x00040252
		// (set) Token: 0x06001541 RID: 5441 RVA: 0x0004205A File Offset: 0x0004025A
		internal string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001542 RID: 5442 RVA: 0x00042063 File Offset: 0x00040263
		// (set) Token: 0x06001543 RID: 5443 RVA: 0x0004206B File Offset: 0x0004026B
		internal XmlNode Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001544 RID: 5444 RVA: 0x00042074 File Offset: 0x00040274
		// (set) Token: 0x06001545 RID: 5445 RVA: 0x0004207C File Offset: 0x0004027C
		internal bool IsNull
		{
			get
			{
				return this.isNull;
			}
			set
			{
				this.isNull = value;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001546 RID: 5446 RVA: 0x00042085 File Offset: 0x00040285
		// (set) Token: 0x06001547 RID: 5447 RVA: 0x0004208D File Offset: 0x0004028D
		internal string UseResultFromOperationId
		{
			get
			{
				return this.useResultFromOperationId;
			}
			set
			{
				this.useResultFromOperationId = value;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001548 RID: 5448 RVA: 0x00042096 File Offset: 0x00040296
		// (set) Token: 0x06001549 RID: 5449 RVA: 0x0004209E File Offset: 0x0004029E
		internal string PropertyName
		{
			get
			{
				return this.propertyName;
			}
			set
			{
				this.propertyName = value;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x000420A7 File Offset: 0x000402A7
		// (set) Token: 0x0600154B RID: 5451 RVA: 0x000420AF File Offset: 0x000402AF
		internal string Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x0600154C RID: 5452 RVA: 0x000420B8 File Offset: 0x000402B8
		// (set) Token: 0x0600154D RID: 5453 RVA: 0x000420C0 File Offset: 0x000402C0
		internal int Index
		{
			get
			{
				return this.index;
			}
			set
			{
				this.index = value;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x000420C9 File Offset: 0x000402C9
		// (set) Token: 0x0600154F RID: 5455 RVA: 0x000420D1 File Offset: 0x000402D1
		internal bool UseFile
		{
			get
			{
				return this.useFile;
			}
			set
			{
				this.useFile = value;
			}
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x000420DC File Offset: 0x000402DC
		public override string ToString()
		{
			return string.Format("Name={0}, UseResultFromOperationId={1}, PropertyName={2}, Type={3}, Index={4}, IsNull={5}, Value={6}, UseFile={7}", new object[]
			{
				this.Name,
				this.UseResultFromOperationId,
				this.PropertyName,
				this.Type,
				this.Index,
				this.IsNull,
				(this.Value == null) ? string.Empty : this.Value.InnerXml,
				this.UseFile
			});
		}

		// Token: 0x04000A51 RID: 2641
		private string name;

		// Token: 0x04000A52 RID: 2642
		private XmlNode value;

		// Token: 0x04000A53 RID: 2643
		private bool isNull;

		// Token: 0x04000A54 RID: 2644
		private string useResultFromOperationId;

		// Token: 0x04000A55 RID: 2645
		private string propertyName;

		// Token: 0x04000A56 RID: 2646
		private string type;

		// Token: 0x04000A57 RID: 2647
		private int index;

		// Token: 0x04000A58 RID: 2648
		private bool useFile;
	}
}
