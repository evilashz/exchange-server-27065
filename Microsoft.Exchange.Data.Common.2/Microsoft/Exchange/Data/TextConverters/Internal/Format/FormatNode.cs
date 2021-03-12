using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Format
{
	// Token: 0x0200029C RID: 668
	internal struct FormatNode
	{
		// Token: 0x06001A96 RID: 6806 RVA: 0x000CFF21 File Offset: 0x000CE121
		internal FormatNode(FormatStore.NodeStore nodes, int nodeHandle)
		{
			this.nodes = nodes;
			this.nodeHandle = nodeHandle;
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x000CFF31 File Offset: 0x000CE131
		internal FormatNode(FormatStore store, int nodeHandle)
		{
			this.nodes = store.Nodes;
			this.nodeHandle = nodeHandle;
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001A98 RID: 6808 RVA: 0x000CFF46 File Offset: 0x000CE146
		public int Handle
		{
			get
			{
				return this.nodeHandle;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001A99 RID: 6809 RVA: 0x000CFF4E File Offset: 0x000CE14E
		public bool IsNull
		{
			get
			{
				return this.nodeHandle == 0;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001A9A RID: 6810 RVA: 0x000CFF59 File Offset: 0x000CE159
		public bool IsInOrder
		{
			get
			{
				return 0 == (byte)(this.nodes.Plane(this.nodeHandle)[this.nodes.Index(this.nodeHandle)].NodeFlags & FormatStore.NodeFlags.OutOfOrder);
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001A9B RID: 6811 RVA: 0x000CFF8D File Offset: 0x000CE18D
		public bool OnRightEdge
		{
			get
			{
				return 0 != (byte)(this.nodes.Plane(this.nodeHandle)[this.nodes.Index(this.nodeHandle)].NodeFlags & FormatStore.NodeFlags.OnRightEdge);
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001A9C RID: 6812 RVA: 0x000CFFC4 File Offset: 0x000CE1C4
		public bool OnLeftEdge
		{
			get
			{
				return 0 != (byte)(this.nodes.Plane(this.nodeHandle)[this.nodes.Index(this.nodeHandle)].NodeFlags & FormatStore.NodeFlags.OnLeftEdge);
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001A9D RID: 6813 RVA: 0x000CFFFB File Offset: 0x000CE1FB
		public bool IsVisited
		{
			get
			{
				return 0 != (byte)(this.nodes.Plane(this.nodeHandle)[this.nodes.Index(this.nodeHandle)].NodeFlags & FormatStore.NodeFlags.Visited);
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001A9E RID: 6814 RVA: 0x000D0033 File Offset: 0x000CE233
		public bool IsEmptyBlockNode
		{
			get
			{
				return (byte)(this.NodeType & FormatContainerType.BlockFlag) != 0 && this.BeginTextPosition + 1U == this.EndTextPosition;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06001A9F RID: 6815 RVA: 0x000D0056 File Offset: 0x000CE256
		public bool CanFlush
		{
			get
			{
				return 0 != (byte)(this.nodes.Plane(this.nodeHandle)[this.nodes.Index(this.nodeHandle)].NodeFlags & FormatStore.NodeFlags.CanFlush);
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001AA0 RID: 6816 RVA: 0x000D008D File Offset: 0x000CE28D
		// (set) Token: 0x06001AA1 RID: 6817 RVA: 0x000D00BB File Offset: 0x000CE2BB
		public FormatContainerType NodeType
		{
			get
			{
				return this.nodes.Plane(this.nodeHandle)[this.nodes.Index(this.nodeHandle)].Type;
			}
			set
			{
				this.nodes.Plane(this.nodeHandle)[this.nodes.Index(this.nodeHandle)].Type = value;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001AA2 RID: 6818 RVA: 0x000D00EA File Offset: 0x000CE2EA
		public bool IsText
		{
			get
			{
				return this.nodes.Plane(this.nodeHandle)[this.nodes.Index(this.nodeHandle)].Type == FormatContainerType.Text;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001AA3 RID: 6819 RVA: 0x000D011C File Offset: 0x000CE31C
		public FormatNode Parent
		{
			get
			{
				int parent = this.nodes.Plane(this.nodeHandle)[this.nodes.Index(this.nodeHandle)].Parent;
				if (parent != 0)
				{
					return new FormatNode(this.nodes, parent);
				}
				return FormatNode.Null;
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001AA4 RID: 6820 RVA: 0x000D016B File Offset: 0x000CE36B
		public bool IsOnlySibling
		{
			get
			{
				return this.nodes.Plane(this.nodeHandle)[this.nodes.Index(this.nodeHandle)].NextSibling == this.nodeHandle;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001AA5 RID: 6821 RVA: 0x000D01A4 File Offset: 0x000CE3A4
		public FormatNode FirstChild
		{
			get
			{
				int lastChild = this.nodes.Plane(this.nodeHandle)[this.nodes.Index(this.nodeHandle)].LastChild;
				if (lastChild != 0)
				{
					return new FormatNode(this.nodes, this.nodes.Plane(lastChild)[this.nodes.Index(lastChild)].NextSibling);
				}
				return FormatNode.Null;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001AA6 RID: 6822 RVA: 0x000D0214 File Offset: 0x000CE414
		public FormatNode LastChild
		{
			get
			{
				int lastChild = this.nodes.Plane(this.nodeHandle)[this.nodes.Index(this.nodeHandle)].LastChild;
				if (lastChild != 0)
				{
					return new FormatNode(this.nodes, lastChild);
				}
				return FormatNode.Null;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001AA7 RID: 6823 RVA: 0x000D0264 File Offset: 0x000CE464
		public FormatNode NextSibling
		{
			get
			{
				int parent = this.nodes.Plane(this.nodeHandle)[this.nodes.Index(this.nodeHandle)].Parent;
				if (parent != 0 && this.nodeHandle != this.nodes.Plane(parent)[this.nodes.Index(parent)].LastChild)
				{
					return new FormatNode(this.nodes, this.nodes.Plane(this.nodeHandle)[this.nodes.Index(this.nodeHandle)].NextSibling);
				}
				return FormatNode.Null;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001AA8 RID: 6824 RVA: 0x000D0308 File Offset: 0x000CE508
		public FormatNode PreviousSibling
		{
			get
			{
				FormatNode formatNode = this.Parent.FirstChild;
				if (this == formatNode)
				{
					return FormatNode.Null;
				}
				while (formatNode.NextSibling != this)
				{
					formatNode = formatNode.NextSibling;
				}
				return formatNode;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x000D0355 File Offset: 0x000CE555
		// (set) Token: 0x06001AAA RID: 6826 RVA: 0x000D0383 File Offset: 0x000CE583
		public uint BeginTextPosition
		{
			get
			{
				return this.nodes.Plane(this.nodeHandle)[this.nodes.Index(this.nodeHandle)].BeginTextPosition;
			}
			set
			{
				this.nodes.Plane(this.nodeHandle)[this.nodes.Index(this.nodeHandle)].BeginTextPosition = value;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x000D03B2 File Offset: 0x000CE5B2
		// (set) Token: 0x06001AAC RID: 6828 RVA: 0x000D03E0 File Offset: 0x000CE5E0
		public uint EndTextPosition
		{
			get
			{
				return this.nodes.Plane(this.nodeHandle)[this.nodes.Index(this.nodeHandle)].EndTextPosition;
			}
			set
			{
				this.nodes.Plane(this.nodeHandle)[this.nodes.Index(this.nodeHandle)].EndTextPosition = value;
			}
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001AAD RID: 6829 RVA: 0x000D040F File Offset: 0x000CE60F
		// (set) Token: 0x06001AAE RID: 6830 RVA: 0x000D043D File Offset: 0x000CE63D
		public int InheritanceMaskIndex
		{
			get
			{
				return this.nodes.Plane(this.nodeHandle)[this.nodes.Index(this.nodeHandle)].InheritanceMaskIndex;
			}
			set
			{
				this.nodes.Plane(this.nodeHandle)[this.nodes.Index(this.nodeHandle)].InheritanceMaskIndex = value;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x000D046C File Offset: 0x000CE66C
		// (set) Token: 0x06001AB0 RID: 6832 RVA: 0x000D049A File Offset: 0x000CE69A
		public FlagProperties FlagProperties
		{
			get
			{
				return this.nodes.Plane(this.nodeHandle)[this.nodes.Index(this.nodeHandle)].FlagProperties;
			}
			set
			{
				this.nodes.Plane(this.nodeHandle)[this.nodes.Index(this.nodeHandle)].FlagProperties = value;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x000D04C9 File Offset: 0x000CE6C9
		// (set) Token: 0x06001AB2 RID: 6834 RVA: 0x000D04F7 File Offset: 0x000CE6F7
		public PropertyBitMask PropertyMask
		{
			get
			{
				return this.nodes.Plane(this.nodeHandle)[this.nodes.Index(this.nodeHandle)].PropertyMask;
			}
			set
			{
				this.nodes.Plane(this.nodeHandle)[this.nodes.Index(this.nodeHandle)].PropertyMask = value;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x000D0526 File Offset: 0x000CE726
		// (set) Token: 0x06001AB4 RID: 6836 RVA: 0x000D0554 File Offset: 0x000CE754
		public Property[] Properties
		{
			get
			{
				return this.nodes.Plane(this.nodeHandle)[this.nodes.Index(this.nodeHandle)].Properties;
			}
			set
			{
				this.nodes.Plane(this.nodeHandle)[this.nodes.Index(this.nodeHandle)].Properties = value;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x000D0583 File Offset: 0x000CE783
		public NodePropertiesEnumerator PropertiesEnumerator
		{
			get
			{
				return new NodePropertiesEnumerator(this);
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001AB6 RID: 6838 RVA: 0x000D0590 File Offset: 0x000CE790
		public bool IsBlockNode
		{
			get
			{
				return 0 != (byte)(this.nodes.Plane(this.nodeHandle)[this.nodes.Index(this.nodeHandle)].Type & FormatContainerType.BlockFlag);
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001AB7 RID: 6839 RVA: 0x000D05CB File Offset: 0x000CE7CB
		public FormatNode.NodeSubtree Subtree
		{
			get
			{
				return new FormatNode.NodeSubtree(this);
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001AB8 RID: 6840 RVA: 0x000D05D8 File Offset: 0x000CE7D8
		public FormatNode.NodeChildren Children
		{
			get
			{
				return new FormatNode.NodeChildren(this);
			}
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x000D05E5 File Offset: 0x000CE7E5
		public static bool operator ==(FormatNode x, FormatNode y)
		{
			return x.nodes == y.nodes && x.nodeHandle == y.nodeHandle;
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x000D0609 File Offset: 0x000CE809
		public static bool operator !=(FormatNode x, FormatNode y)
		{
			return x.nodes != y.nodes || x.nodeHandle != y.nodeHandle;
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x000D0630 File Offset: 0x000CE830
		public void SetOutOfOrder()
		{
			FormatStore.NodeEntry[] array = this.nodes.Plane(this.nodeHandle);
			int num = this.nodes.Index(this.nodeHandle);
			array[num].NodeFlags = (array[num].NodeFlags | FormatStore.NodeFlags.OutOfOrder);
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x000D0667 File Offset: 0x000CE867
		public void SetOnLeftEdge()
		{
			FormatStore.NodeEntry[] array = this.nodes.Plane(this.nodeHandle);
			int num = this.nodes.Index(this.nodeHandle);
			array[num].NodeFlags = (array[num].NodeFlags | FormatStore.NodeFlags.OnLeftEdge);
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x000D069E File Offset: 0x000CE89E
		public void ResetOnLeftEdge()
		{
			FormatStore.NodeEntry[] array = this.nodes.Plane(this.nodeHandle);
			int num = this.nodes.Index(this.nodeHandle);
			array[num].NodeFlags = (array[num].NodeFlags & ~FormatStore.NodeFlags.OnLeftEdge);
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x000D06D9 File Offset: 0x000CE8D9
		public void SetOnRightEdge()
		{
			FormatStore.NodeEntry[] array = this.nodes.Plane(this.nodeHandle);
			int num = this.nodes.Index(this.nodeHandle);
			array[num].NodeFlags = (array[num].NodeFlags | FormatStore.NodeFlags.OnRightEdge);
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x000D0710 File Offset: 0x000CE910
		public void SetVisited()
		{
			FormatStore.NodeEntry[] array = this.nodes.Plane(this.nodeHandle);
			int num = this.nodes.Index(this.nodeHandle);
			array[num].NodeFlags = (array[num].NodeFlags | FormatStore.NodeFlags.Visited);
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x000D0748 File Offset: 0x000CE948
		public void ResetVisited()
		{
			FormatStore.NodeEntry[] array = this.nodes.Plane(this.nodeHandle);
			int num = this.nodes.Index(this.nodeHandle);
			array[num].NodeFlags = (array[num].NodeFlags & ~FormatStore.NodeFlags.Visited);
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x000D0784 File Offset: 0x000CE984
		public PropertyValue GetProperty(PropertyId id)
		{
			FormatStore.NodeEntry[] array = this.nodes.Plane(this.nodeHandle);
			int num = this.nodes.Index(this.nodeHandle);
			if (FlagProperties.IsFlagProperty(id))
			{
				return array[num].FlagProperties.GetPropertyValue(id);
			}
			if (array[num].PropertyMask.IsSet(id))
			{
				for (int i = 0; i < array[num].Properties.Length; i++)
				{
					Property property = array[num].Properties[i];
					if (property.Id == id)
					{
						return property.Value;
					}
					if (property.Id > id)
					{
						break;
					}
				}
			}
			return PropertyValue.Null;
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x000D0838 File Offset: 0x000CEA38
		public void SetProperty(PropertyId id, PropertyValue value)
		{
			FormatStore.NodeEntry[] array = this.nodes.Plane(this.nodeHandle);
			int num = this.nodes.Index(this.nodeHandle);
			if (FlagProperties.IsFlagProperty(id))
			{
				array[num].FlagProperties.SetPropertyValue(id, value);
				return;
			}
			int i = 0;
			if (array[num].Properties != null)
			{
				while (i < array[num].Properties.Length)
				{
					Property property = array[num].Properties[i];
					if (property.Id == id)
					{
						array[num].Properties[i].Set(id, value);
						return;
					}
					if (property.Id > id)
					{
						break;
					}
					i++;
				}
			}
			if (array[num].Properties == null)
			{
				array[num].Properties = new Property[1];
				array[num].Properties[0].Set(id, value);
				array[num].PropertyMask.Set(id);
				return;
			}
			Property[] array2 = new Property[array[num].Properties.Length + 1];
			if (i != 0)
			{
				Array.Copy(array[num].Properties, 0, array2, 0, i);
			}
			if (i != array[num].Properties.Length)
			{
				Array.Copy(array[num].Properties, i, array2, i + 1, array[num].Properties.Length - i);
			}
			array2[i].Set(id, value);
			array[num].Properties = array2;
			array[num].PropertyMask.Set(id);
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x000D09D5 File Offset: 0x000CEBD5
		public void AppendChild(FormatNode newChildNode)
		{
			FormatNode.InternalAppendChild(this.nodes, this.nodeHandle, newChildNode.Handle);
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x000D09EF File Offset: 0x000CEBEF
		public void PrependChild(FormatNode newChildNode)
		{
			FormatNode.InternalPrependChild(this.nodes, this.nodeHandle, newChildNode.Handle);
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x000D0A0C File Offset: 0x000CEC0C
		public void InsertSiblingAfter(FormatNode newSiblingNode)
		{
			int num = newSiblingNode.nodeHandle;
			FormatStore.NodeEntry[] array = this.nodes.Plane(num);
			int num2 = this.nodes.Index(num);
			FormatStore.NodeEntry[] array2 = this.nodes.Plane(this.nodeHandle);
			int num3 = this.nodes.Index(this.nodeHandle);
			int parent = array2[num3].Parent;
			FormatStore.NodeEntry[] array3 = this.nodes.Plane(parent);
			int num4 = this.nodes.Index(parent);
			array[num2].Parent = parent;
			array[num2].NextSibling = array2[num3].NextSibling;
			array2[num3].NextSibling = num;
			if (this.nodeHandle == array3[num4].LastChild)
			{
				array3[num4].LastChild = num;
			}
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x000D0AE8 File Offset: 0x000CECE8
		public void InsertSiblingBefore(FormatNode newSiblingNode)
		{
			int num = newSiblingNode.nodeHandle;
			FormatStore.NodeEntry[] array = this.nodes.Plane(num);
			int num2 = this.nodes.Index(num);
			FormatStore.NodeEntry[] array2 = this.nodes.Plane(this.nodeHandle);
			int num3 = this.nodes.Index(this.nodeHandle);
			int parent = array2[num3].Parent;
			FormatStore.NodeEntry[] array3 = this.nodes.Plane(parent);
			int num4 = this.nodes.Index(parent);
			int handle = array3[num4].LastChild;
			FormatStore.NodeEntry[] array4 = this.nodes.Plane(handle);
			int num5 = this.nodes.Index(handle);
			while (array4[num5].NextSibling != this.nodeHandle)
			{
				handle = array4[num5].NextSibling;
				array4 = this.nodes.Plane(handle);
				num5 = this.nodes.Index(handle);
			}
			array[num2].Parent = parent;
			array[num2].NextSibling = this.nodeHandle;
			array4[num5].NextSibling = num;
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x000D0C0C File Offset: 0x000CEE0C
		public void RemoveFromParent()
		{
			FormatStore.NodeEntry[] array = this.nodes.Plane(this.nodeHandle);
			int num = this.nodes.Index(this.nodeHandle);
			int parent = array[num].Parent;
			FormatStore.NodeEntry[] array2 = this.nodes.Plane(parent);
			int num2 = this.nodes.Index(parent);
			int nextSibling = array[num].NextSibling;
			if (this.nodeHandle == nextSibling)
			{
				array2[num2].LastChild = 0;
			}
			else
			{
				int num3 = array2[num2].LastChild;
				FormatStore.NodeEntry[] array3 = this.nodes.Plane(num3);
				int num4 = this.nodes.Index(num3);
				while (array3[num4].NextSibling != this.nodeHandle)
				{
					num3 = array3[num4].NextSibling;
					array3 = this.nodes.Plane(num3);
					num4 = this.nodes.Index(num3);
				}
				array3[num4].NextSibling = array[num].NextSibling;
				if (array2[num2].LastChild == this.nodeHandle)
				{
					array2[num2].LastChild = num3;
				}
			}
			array[num].Parent = 0;
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x000D0D50 File Offset: 0x000CEF50
		public void MoveAllChildrenToNewParent(FormatNode newParent)
		{
			while (!this.FirstChild.IsNull)
			{
				FormatNode firstChild = this.FirstChild;
				firstChild.RemoveFromParent();
				newParent.AppendChild(firstChild);
			}
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x000D0D85 File Offset: 0x000CEF85
		public void ChangeNodeType(FormatContainerType newType)
		{
			this.nodes.Plane(this.nodeHandle)[this.nodes.Index(this.nodeHandle)].Type = newType;
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x000D0DB4 File Offset: 0x000CEFB4
		public void PrepareToClose(uint endTextPosition)
		{
			FormatStore.NodeEntry[] array = this.nodes.Plane(this.nodeHandle);
			int num = this.nodes.Index(this.nodeHandle);
			array[num].EndTextPosition = endTextPosition;
			FormatStore.NodeEntry[] array2 = array;
			int num2 = num;
			array2[num2].NodeFlags = (array2[num2].NodeFlags & ~FormatStore.NodeFlags.OnRightEdge);
			FormatStore.NodeEntry[] array3 = array;
			int num3 = num;
			array3[num3].NodeFlags = (array3[num3].NodeFlags | FormatStore.NodeFlags.CanFlush);
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x000D0E20 File Offset: 0x000CF020
		public void SetProps(FlagProperties flagProperties, PropertyBitMask propertyMask, Property[] properties, int inheritanceMaskIndex)
		{
			FormatStore.NodeEntry[] array = this.nodes.Plane(this.nodeHandle);
			int num = this.nodes.Index(this.nodeHandle);
			array[num].FlagProperties = flagProperties;
			array[num].PropertyMask = propertyMask;
			array[num].Properties = properties;
			array[num].InheritanceMaskIndex = inheritanceMaskIndex;
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x000D0E88 File Offset: 0x000CF088
		public FormatNode SplitTextNode(uint splitPosition)
		{
			FormatStore.NodeEntry[] array = this.nodes.Plane(this.nodeHandle);
			int num = this.nodes.Index(this.nodeHandle);
			int handle = this.nodes.Allocate(FormatContainerType.Text, array[num].BeginTextPosition);
			FormatStore.NodeEntry[] array2 = this.nodes.Plane(handle);
			int num2 = this.nodes.Index(handle);
			array2[num2].NodeFlags = array[num].NodeFlags;
			array2[num2].TextMapping = array[num].TextMapping;
			array2[num2].EndTextPosition = splitPosition;
			array2[num2].FlagProperties = array[num].FlagProperties;
			array2[num2].PropertyMask = array[num].PropertyMask;
			array2[num2].Properties = array[num].Properties;
			array[num].BeginTextPosition = splitPosition;
			FormatNode formatNode = new FormatNode(this.nodes, handle);
			this.InsertSiblingBefore(formatNode);
			return formatNode;
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x000D0FA0 File Offset: 0x000CF1A0
		public FormatNode SplitNodeBeforeChild(FormatNode child)
		{
			FormatStore.NodeEntry[] array = this.nodes.Plane(this.nodeHandle);
			int num = this.nodes.Index(this.nodeHandle);
			int handle = this.nodes.Allocate(this.NodeType, array[num].BeginTextPosition);
			FormatStore.NodeEntry[] array2 = this.nodes.Plane(handle);
			int num2 = this.nodes.Index(handle);
			array2[num2].NodeFlags = array[num].NodeFlags;
			array2[num2].TextMapping = array[num].TextMapping;
			array2[num2].EndTextPosition = child.BeginTextPosition;
			array2[num2].FlagProperties = array[num].FlagProperties;
			array2[num2].PropertyMask = array[num].PropertyMask;
			array2[num2].Properties = array[num].Properties;
			array[num].BeginTextPosition = child.BeginTextPosition;
			FormatNode formatNode = new FormatNode(this.nodes, handle);
			do
			{
				FormatNode firstChild = this.FirstChild;
				firstChild.RemoveFromParent();
				formatNode.AppendChild(firstChild);
			}
			while (this.FirstChild != child);
			this.InsertSiblingBefore(formatNode);
			return formatNode;
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x000D10EC File Offset: 0x000CF2EC
		public FormatNode DuplicateInsertAsChild()
		{
			int handle = this.nodes.Allocate(this.NodeType, this.BeginTextPosition);
			FormatStore.NodeEntry[] array = this.nodes.Plane(this.nodeHandle);
			int num = this.nodes.Index(this.nodeHandle);
			FormatStore.NodeEntry[] array2 = this.nodes.Plane(handle);
			int num2 = this.nodes.Index(handle);
			array2[num2].NodeFlags = array[num].NodeFlags;
			array2[num2].TextMapping = array[num].TextMapping;
			array2[num2].EndTextPosition = array[num].EndTextPosition;
			array2[num2].FlagProperties = array[num].FlagProperties;
			array2[num2].PropertyMask = array[num].PropertyMask;
			array2[num2].Properties = array[num].Properties;
			FormatNode formatNode = new FormatNode(this.nodes, handle);
			this.MoveAllChildrenToNewParent(formatNode);
			this.AppendChild(formatNode);
			return formatNode;
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x000D1206 File Offset: 0x000CF406
		public override bool Equals(object obj)
		{
			return obj is FormatNode && this.nodes == ((FormatNode)obj).nodes && this.nodeHandle == ((FormatNode)obj).nodeHandle;
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x000D1238 File Offset: 0x000CF438
		public override int GetHashCode()
		{
			return this.nodeHandle;
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x000D1240 File Offset: 0x000CF440
		internal static void InternalAppendChild(FormatStore.NodeStore nodes, int thisNode, int newChildNode)
		{
			FormatNode.InternalPrependChild(nodes, thisNode, newChildNode);
			nodes.Plane(thisNode)[nodes.Index(thisNode)].LastChild = newChildNode;
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x000D1264 File Offset: 0x000CF464
		internal static void InternalPrependChild(FormatStore.NodeStore nodes, int thisNode, int newChildNode)
		{
			FormatStore.NodeEntry[] array = nodes.Plane(thisNode);
			int num = nodes.Index(thisNode);
			FormatStore.NodeEntry[] array2 = nodes.Plane(newChildNode);
			int num2 = nodes.Index(newChildNode);
			if (array[num].LastChild != 0)
			{
				int lastChild = array[num].LastChild;
				FormatStore.NodeEntry[] array3 = nodes.Plane(lastChild);
				int num3 = nodes.Index(lastChild);
				array2[num2].NextSibling = array3[num3].NextSibling;
				array3[num3].NextSibling = newChildNode;
				array2[num2].Parent = thisNode;
				return;
			}
			array2[num2].NextSibling = newChildNode;
			array2[num2].Parent = thisNode;
			array[num].LastChild = newChildNode;
		}

		// Token: 0x04002090 RID: 8336
		public static readonly FormatNode Null = default(FormatNode);

		// Token: 0x04002091 RID: 8337
		private FormatStore.NodeStore nodes;

		// Token: 0x04002092 RID: 8338
		private int nodeHandle;

		// Token: 0x0200029D RID: 669
		internal struct NodeSubtree : IEnumerable<FormatNode>, IEnumerable
		{
			// Token: 0x06001AD4 RID: 6868 RVA: 0x000D132C File Offset: 0x000CF52C
			internal NodeSubtree(FormatNode node)
			{
				this.node = node;
			}

			// Token: 0x06001AD5 RID: 6869 RVA: 0x000D1335 File Offset: 0x000CF535
			public FormatNode.SubtreeEnumerator GetEnumerator()
			{
				return new FormatNode.SubtreeEnumerator(this.node, false);
			}

			// Token: 0x06001AD6 RID: 6870 RVA: 0x000D1343 File Offset: 0x000CF543
			public FormatNode.SubtreeEnumerator GetEnumerator(bool revisitParent)
			{
				return new FormatNode.SubtreeEnumerator(this.node, revisitParent);
			}

			// Token: 0x06001AD7 RID: 6871 RVA: 0x000D1351 File Offset: 0x000CF551
			IEnumerator<FormatNode> IEnumerable<FormatNode>.GetEnumerator()
			{
				return new FormatNode.SubtreeEnumerator(this.node, false);
			}

			// Token: 0x06001AD8 RID: 6872 RVA: 0x000D1364 File Offset: 0x000CF564
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new FormatNode.SubtreeEnumerator(this.node, false);
			}

			// Token: 0x04002093 RID: 8339
			private FormatNode node;
		}

		// Token: 0x0200029E RID: 670
		internal struct NodeChildren : IEnumerable<FormatNode>, IEnumerable
		{
			// Token: 0x06001AD9 RID: 6873 RVA: 0x000D1377 File Offset: 0x000CF577
			internal NodeChildren(FormatNode node)
			{
				this.node = node;
			}

			// Token: 0x06001ADA RID: 6874 RVA: 0x000D1380 File Offset: 0x000CF580
			public FormatNode.ChildrenEnumerator GetEnumerator()
			{
				return new FormatNode.ChildrenEnumerator(this.node);
			}

			// Token: 0x06001ADB RID: 6875 RVA: 0x000D138D File Offset: 0x000CF58D
			IEnumerator<FormatNode> IEnumerable<FormatNode>.GetEnumerator()
			{
				return new FormatNode.ChildrenEnumerator(this.node);
			}

			// Token: 0x06001ADC RID: 6876 RVA: 0x000D139F File Offset: 0x000CF59F
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new FormatNode.ChildrenEnumerator(this.node);
			}

			// Token: 0x04002094 RID: 8340
			private FormatNode node;
		}

		// Token: 0x0200029F RID: 671
		internal struct ChildrenEnumerator : IEnumerator<FormatNode>, IDisposable, IEnumerator
		{
			// Token: 0x06001ADD RID: 6877 RVA: 0x000D13B1 File Offset: 0x000CF5B1
			internal ChildrenEnumerator(FormatNode node)
			{
				this.node = node;
				this.current = FormatNode.Null;
				this.next = this.node.FirstChild;
			}

			// Token: 0x170006F5 RID: 1781
			// (get) Token: 0x06001ADE RID: 6878 RVA: 0x000D13D6 File Offset: 0x000CF5D6
			public FormatNode Current
			{
				get
				{
					if (this.current.IsNull)
					{
						throw new InvalidOperationException(this.next.IsNull ? "Strings.ErrorAfterLast" : "Strings.ErrorBeforeFirst");
					}
					return this.current;
				}
			}

			// Token: 0x170006F6 RID: 1782
			// (get) Token: 0x06001ADF RID: 6879 RVA: 0x000D140A File Offset: 0x000CF60A
			object IEnumerator.Current
			{
				get
				{
					if (this.current.IsNull)
					{
						throw new InvalidOperationException(this.next.IsNull ? "Strings.ErrorAfterLast" : "Strings.ErrorBeforeFirst");
					}
					return this.current;
				}
			}

			// Token: 0x06001AE0 RID: 6880 RVA: 0x000D1443 File Offset: 0x000CF643
			public bool MoveNext()
			{
				this.current = this.next;
				if (this.current.IsNull)
				{
					return false;
				}
				this.next = this.current.NextSibling;
				return true;
			}

			// Token: 0x06001AE1 RID: 6881 RVA: 0x000D1472 File Offset: 0x000CF672
			public void Reset()
			{
				this.current = FormatNode.Null;
				this.next = this.node.FirstChild;
			}

			// Token: 0x06001AE2 RID: 6882 RVA: 0x000D1490 File Offset: 0x000CF690
			public void Dispose()
			{
				this.Reset();
			}

			// Token: 0x04002095 RID: 8341
			private FormatNode node;

			// Token: 0x04002096 RID: 8342
			private FormatNode current;

			// Token: 0x04002097 RID: 8343
			private FormatNode next;
		}

		// Token: 0x020002A0 RID: 672
		internal struct SubtreeEnumerator : IEnumerator<FormatNode>, IDisposable, IEnumerator
		{
			// Token: 0x06001AE3 RID: 6883 RVA: 0x000D1498 File Offset: 0x000CF698
			internal SubtreeEnumerator(FormatNode node, bool revisitParent)
			{
				this.revisitParent = revisitParent;
				this.root = node;
				this.current = FormatNode.Null;
				this.currentDisposition = (FormatNode.SubtreeEnumerator.EnumeratorDisposition)0;
				this.nextChild = node;
				this.depth = -1;
			}

			// Token: 0x170006F7 RID: 1783
			// (get) Token: 0x06001AE4 RID: 6884 RVA: 0x000D14C8 File Offset: 0x000CF6C8
			public FormatNode Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x170006F8 RID: 1784
			// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x000D14D0 File Offset: 0x000CF6D0
			public bool FirstVisit
			{
				get
				{
					return 0 != (byte)(this.currentDisposition & FormatNode.SubtreeEnumerator.EnumeratorDisposition.Begin);
				}
			}

			// Token: 0x170006F9 RID: 1785
			// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x000D14E1 File Offset: 0x000CF6E1
			public bool LastVisit
			{
				get
				{
					return 0 != (byte)(this.currentDisposition & FormatNode.SubtreeEnumerator.EnumeratorDisposition.End);
				}
			}

			// Token: 0x170006FA RID: 1786
			// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x000D14F2 File Offset: 0x000CF6F2
			public int Depth
			{
				get
				{
					return this.depth;
				}
			}

			// Token: 0x170006FB RID: 1787
			// (get) Token: 0x06001AE8 RID: 6888 RVA: 0x000D14FA File Offset: 0x000CF6FA
			object IEnumerator.Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x06001AE9 RID: 6889 RVA: 0x000D1508 File Offset: 0x000CF708
			public bool MoveNext()
			{
				if (this.nextChild != FormatNode.Null)
				{
					this.depth++;
					this.current = this.nextChild;
					this.nextChild = this.current.FirstChild;
					this.currentDisposition = (FormatNode.SubtreeEnumerator.EnumeratorDisposition)(1 | ((this.nextChild == FormatNode.Null) ? 2 : 0));
					return true;
				}
				if (this.depth < 0)
				{
					return false;
				}
				for (;;)
				{
					this.depth--;
					if (this.depth < 0)
					{
						break;
					}
					this.nextChild = this.current.NextSibling;
					this.current = this.current.Parent;
					this.currentDisposition = ((this.nextChild == FormatNode.Null) ? FormatNode.SubtreeEnumerator.EnumeratorDisposition.End : ((FormatNode.SubtreeEnumerator.EnumeratorDisposition)0));
					if (this.revisitParent || !(this.nextChild == FormatNode.Null))
					{
						goto IL_FA;
					}
				}
				this.current = FormatNode.Null;
				this.nextChild = FormatNode.Null;
				this.currentDisposition = (FormatNode.SubtreeEnumerator.EnumeratorDisposition)0;
				return false;
				IL_FA:
				return this.revisitParent || this.MoveNext();
			}

			// Token: 0x06001AEA RID: 6890 RVA: 0x000D1620 File Offset: 0x000CF820
			public FormatNode PreviewNextNode()
			{
				if (this.nextChild != FormatNode.Null)
				{
					return this.nextChild;
				}
				if (this.depth < 0)
				{
					return FormatNode.Null;
				}
				int num = this.depth;
				FormatNode parent = this.current;
				for (;;)
				{
					num--;
					if (num < 0)
					{
						break;
					}
					FormatNode nextSibling = parent.NextSibling;
					parent = parent.Parent;
					if (this.revisitParent || !(nextSibling == FormatNode.Null))
					{
						goto IL_69;
					}
				}
				return FormatNode.Null;
				IL_69:
				if (!this.revisitParent)
				{
					FormatNode nextSibling;
					return nextSibling;
				}
				return parent;
			}

			// Token: 0x06001AEB RID: 6891 RVA: 0x000D16A1 File Offset: 0x000CF8A1
			public void SkipChildren()
			{
				if (this.nextChild != FormatNode.Null)
				{
					this.nextChild = FormatNode.Null;
					this.currentDisposition |= FormatNode.SubtreeEnumerator.EnumeratorDisposition.End;
				}
			}

			// Token: 0x06001AEC RID: 6892 RVA: 0x000D16CF File Offset: 0x000CF8CF
			void IEnumerator.Reset()
			{
				this.current = FormatNode.Null;
				this.currentDisposition = (FormatNode.SubtreeEnumerator.EnumeratorDisposition)0;
				this.nextChild = this.root;
				this.depth = -1;
			}

			// Token: 0x06001AED RID: 6893 RVA: 0x000D16F6 File Offset: 0x000CF8F6
			void IDisposable.Dispose()
			{
				((IEnumerator)this).Reset();
				GC.SuppressFinalize(this);
			}

			// Token: 0x04002098 RID: 8344
			private bool revisitParent;

			// Token: 0x04002099 RID: 8345
			private FormatNode.SubtreeEnumerator.EnumeratorDisposition currentDisposition;

			// Token: 0x0400209A RID: 8346
			private FormatNode root;

			// Token: 0x0400209B RID: 8347
			private FormatNode current;

			// Token: 0x0400209C RID: 8348
			private FormatNode nextChild;

			// Token: 0x0400209D RID: 8349
			private int depth;

			// Token: 0x020002A1 RID: 673
			[Flags]
			private enum EnumeratorDisposition : byte
			{
				// Token: 0x0400209F RID: 8351
				Begin = 1,
				// Token: 0x040020A0 RID: 8352
				End = 2
			}
		}
	}
}
