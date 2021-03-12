using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;
using System.Text;

namespace System.Security.Policy
{
	// Token: 0x0200033C RID: 828
	[ComVisible(true)]
	[Serializable]
	public sealed class PolicyStatement : ISecurityPolicyEncodable, ISecurityEncodable
	{
		// Token: 0x060029F6 RID: 10742 RVA: 0x0009BFA8 File Offset: 0x0009A1A8
		internal PolicyStatement()
		{
			this.m_permSet = null;
			this.m_attributes = PolicyStatementAttribute.Nothing;
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x0009BFBE File Offset: 0x0009A1BE
		public PolicyStatement(PermissionSet permSet) : this(permSet, PolicyStatementAttribute.Nothing)
		{
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x0009BFC8 File Offset: 0x0009A1C8
		public PolicyStatement(PermissionSet permSet, PolicyStatementAttribute attributes)
		{
			if (permSet == null)
			{
				this.m_permSet = new PermissionSet(false);
			}
			else
			{
				this.m_permSet = permSet.Copy();
			}
			if (PolicyStatement.ValidProperties(attributes))
			{
				this.m_attributes = attributes;
			}
		}

		// Token: 0x060029F9 RID: 10745 RVA: 0x0009BFFC File Offset: 0x0009A1FC
		private PolicyStatement(PermissionSet permSet, PolicyStatementAttribute attributes, bool copy)
		{
			if (permSet != null)
			{
				if (copy)
				{
					this.m_permSet = permSet.Copy();
				}
				else
				{
					this.m_permSet = permSet;
				}
			}
			else
			{
				this.m_permSet = new PermissionSet(false);
			}
			this.m_attributes = attributes;
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x060029FA RID: 10746 RVA: 0x0009C034 File Offset: 0x0009A234
		// (set) Token: 0x060029FB RID: 10747 RVA: 0x0009C078 File Offset: 0x0009A278
		public PermissionSet PermissionSet
		{
			get
			{
				PermissionSet result;
				lock (this)
				{
					result = this.m_permSet.Copy();
				}
				return result;
			}
			set
			{
				lock (this)
				{
					if (value == null)
					{
						this.m_permSet = new PermissionSet(false);
					}
					else
					{
						this.m_permSet = value.Copy();
					}
				}
			}
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x0009C0CC File Offset: 0x0009A2CC
		internal void SetPermissionSetNoCopy(PermissionSet permSet)
		{
			this.m_permSet = permSet;
		}

		// Token: 0x060029FD RID: 10749 RVA: 0x0009C0D8 File Offset: 0x0009A2D8
		internal PermissionSet GetPermissionSetNoCopy()
		{
			PermissionSet permSet;
			lock (this)
			{
				permSet = this.m_permSet;
			}
			return permSet;
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x060029FE RID: 10750 RVA: 0x0009C118 File Offset: 0x0009A318
		// (set) Token: 0x060029FF RID: 10751 RVA: 0x0009C120 File Offset: 0x0009A320
		public PolicyStatementAttribute Attributes
		{
			get
			{
				return this.m_attributes;
			}
			set
			{
				if (PolicyStatement.ValidProperties(value))
				{
					this.m_attributes = value;
				}
			}
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x0009C134 File Offset: 0x0009A334
		public PolicyStatement Copy()
		{
			PolicyStatement policyStatement = new PolicyStatement(this.m_permSet, this.Attributes, true);
			if (this.HasDependentEvidence)
			{
				policyStatement.m_dependentEvidence = new List<IDelayEvaluatedEvidence>(this.m_dependentEvidence);
			}
			return policyStatement;
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06002A01 RID: 10753 RVA: 0x0009C170 File Offset: 0x0009A370
		public string AttributeString
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = true;
				if (this.GetFlag(1))
				{
					stringBuilder.Append("Exclusive");
					flag = false;
				}
				if (this.GetFlag(2))
				{
					if (!flag)
					{
						stringBuilder.Append(" ");
					}
					stringBuilder.Append("LevelFinal");
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x0009C1C6 File Offset: 0x0009A3C6
		private static bool ValidProperties(PolicyStatementAttribute attributes)
		{
			if ((attributes & ~(PolicyStatementAttribute.Exclusive | PolicyStatementAttribute.LevelFinal)) == PolicyStatementAttribute.Nothing)
			{
				return true;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"));
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x0009C1DF File Offset: 0x0009A3DF
		private bool GetFlag(int flag)
		{
			return (flag & (int)this.m_attributes) != 0;
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06002A04 RID: 10756 RVA: 0x0009C1EC File Offset: 0x0009A3EC
		internal IEnumerable<IDelayEvaluatedEvidence> DependentEvidence
		{
			get
			{
				return this.m_dependentEvidence.AsReadOnly();
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06002A05 RID: 10757 RVA: 0x0009C1F9 File Offset: 0x0009A3F9
		internal bool HasDependentEvidence
		{
			get
			{
				return this.m_dependentEvidence != null && this.m_dependentEvidence.Count > 0;
			}
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x0009C213 File Offset: 0x0009A413
		internal void AddDependentEvidence(IDelayEvaluatedEvidence dependentEvidence)
		{
			if (this.m_dependentEvidence == null)
			{
				this.m_dependentEvidence = new List<IDelayEvaluatedEvidence>();
			}
			this.m_dependentEvidence.Add(dependentEvidence);
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x0009C234 File Offset: 0x0009A434
		internal void InplaceUnion(PolicyStatement childPolicy)
		{
			if ((this.Attributes & childPolicy.Attributes & PolicyStatementAttribute.Exclusive) == PolicyStatementAttribute.Exclusive)
			{
				throw new PolicyException(Environment.GetResourceString("Policy_MultipleExclusive"));
			}
			if (childPolicy.HasDependentEvidence)
			{
				bool flag = this.m_permSet.IsSubsetOf(childPolicy.GetPermissionSetNoCopy()) && !childPolicy.GetPermissionSetNoCopy().IsSubsetOf(this.m_permSet);
				if (this.HasDependentEvidence || flag)
				{
					if (this.m_dependentEvidence == null)
					{
						this.m_dependentEvidence = new List<IDelayEvaluatedEvidence>();
					}
					this.m_dependentEvidence.AddRange(childPolicy.DependentEvidence);
				}
			}
			if ((childPolicy.Attributes & PolicyStatementAttribute.Exclusive) == PolicyStatementAttribute.Exclusive)
			{
				this.m_permSet = childPolicy.GetPermissionSetNoCopy();
				this.Attributes = childPolicy.Attributes;
				return;
			}
			this.m_permSet.InplaceUnion(childPolicy.GetPermissionSetNoCopy());
			this.Attributes |= childPolicy.Attributes;
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x0009C30C File Offset: 0x0009A50C
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x06002A09 RID: 10761 RVA: 0x0009C315 File Offset: 0x0009A515
		public void FromXml(SecurityElement et)
		{
			this.FromXml(et, null);
		}

		// Token: 0x06002A0A RID: 10762 RVA: 0x0009C31F File Offset: 0x0009A51F
		public SecurityElement ToXml(PolicyLevel level)
		{
			return this.ToXml(level, false);
		}

		// Token: 0x06002A0B RID: 10763 RVA: 0x0009C32C File Offset: 0x0009A52C
		internal SecurityElement ToXml(PolicyLevel level, bool useInternal)
		{
			SecurityElement securityElement = new SecurityElement("PolicyStatement");
			securityElement.AddAttribute("version", "1");
			if (this.m_attributes != PolicyStatementAttribute.Nothing)
			{
				securityElement.AddAttribute("Attributes", XMLUtil.BitFieldEnumToString(typeof(PolicyStatementAttribute), this.m_attributes));
			}
			lock (this)
			{
				if (this.m_permSet != null)
				{
					if (this.m_permSet is NamedPermissionSet)
					{
						NamedPermissionSet namedPermissionSet = (NamedPermissionSet)this.m_permSet;
						if (level != null && level.GetNamedPermissionSet(namedPermissionSet.Name) != null)
						{
							securityElement.AddAttribute("PermissionSetName", namedPermissionSet.Name);
						}
						else if (useInternal)
						{
							securityElement.AddChild(namedPermissionSet.InternalToXml());
						}
						else
						{
							securityElement.AddChild(namedPermissionSet.ToXml());
						}
					}
					else if (useInternal)
					{
						securityElement.AddChild(this.m_permSet.InternalToXml());
					}
					else
					{
						securityElement.AddChild(this.m_permSet.ToXml());
					}
				}
			}
			return securityElement;
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x0009C438 File Offset: 0x0009A638
		[SecuritySafeCritical]
		public void FromXml(SecurityElement et, PolicyLevel level)
		{
			this.FromXml(et, level, false);
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x0009C444 File Offset: 0x0009A644
		[SecurityCritical]
		internal void FromXml(SecurityElement et, PolicyLevel level, bool allowInternalOnly)
		{
			if (et == null)
			{
				throw new ArgumentNullException("et");
			}
			if (!et.Tag.Equals("PolicyStatement"))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidXMLElement"), "PolicyStatement", base.GetType().FullName));
			}
			this.m_attributes = PolicyStatementAttribute.Nothing;
			string text = et.Attribute("Attributes");
			if (text != null)
			{
				this.m_attributes = (PolicyStatementAttribute)Enum.Parse(typeof(PolicyStatementAttribute), text);
			}
			lock (this)
			{
				this.m_permSet = null;
				if (level != null)
				{
					string text2 = et.Attribute("PermissionSetName");
					if (text2 != null)
					{
						this.m_permSet = level.GetNamedPermissionSetInternal(text2);
						if (this.m_permSet == null)
						{
							this.m_permSet = new PermissionSet(PermissionState.None);
						}
					}
				}
				if (this.m_permSet == null)
				{
					SecurityElement securityElement = et.SearchForChildByTag("PermissionSet");
					if (securityElement != null)
					{
						string text3 = securityElement.Attribute("class");
						if (text3 != null && (text3.Equals("NamedPermissionSet") || text3.Equals("System.Security.NamedPermissionSet")))
						{
							this.m_permSet = new NamedPermissionSet("DefaultName", PermissionState.None);
						}
						else
						{
							this.m_permSet = new PermissionSet(PermissionState.None);
						}
						try
						{
							this.m_permSet.FromXml(securityElement, allowInternalOnly, true);
							goto IL_14F;
						}
						catch
						{
							goto IL_14F;
						}
					}
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXML"));
				}
				IL_14F:
				if (this.m_permSet == null)
				{
					this.m_permSet = new PermissionSet(PermissionState.None);
				}
			}
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x0009C5DC File Offset: 0x0009A7DC
		[SecurityCritical]
		internal void FromXml(SecurityDocument doc, int position, PolicyLevel level, bool allowInternalOnly)
		{
			if (doc == null)
			{
				throw new ArgumentNullException("doc");
			}
			if (!doc.GetTagForElement(position).Equals("PolicyStatement"))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidXMLElement"), "PolicyStatement", base.GetType().FullName));
			}
			this.m_attributes = PolicyStatementAttribute.Nothing;
			string attributeForElement = doc.GetAttributeForElement(position, "Attributes");
			if (attributeForElement != null)
			{
				this.m_attributes = (PolicyStatementAttribute)Enum.Parse(typeof(PolicyStatementAttribute), attributeForElement);
			}
			lock (this)
			{
				this.m_permSet = null;
				if (level != null)
				{
					string attributeForElement2 = doc.GetAttributeForElement(position, "PermissionSetName");
					if (attributeForElement2 != null)
					{
						this.m_permSet = level.GetNamedPermissionSetInternal(attributeForElement2);
						if (this.m_permSet == null)
						{
							this.m_permSet = new PermissionSet(PermissionState.None);
						}
					}
				}
				if (this.m_permSet == null)
				{
					ArrayList childrenPositionForElement = doc.GetChildrenPositionForElement(position);
					int num = -1;
					for (int i = 0; i < childrenPositionForElement.Count; i++)
					{
						if (doc.GetTagForElement((int)childrenPositionForElement[i]).Equals("PermissionSet"))
						{
							num = (int)childrenPositionForElement[i];
						}
					}
					if (num == -1)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXML"));
					}
					string attributeForElement3 = doc.GetAttributeForElement(num, "class");
					if (attributeForElement3 != null && (attributeForElement3.Equals("NamedPermissionSet") || attributeForElement3.Equals("System.Security.NamedPermissionSet")))
					{
						this.m_permSet = new NamedPermissionSet("DefaultName", PermissionState.None);
					}
					else
					{
						this.m_permSet = new PermissionSet(PermissionState.None);
					}
					this.m_permSet.FromXml(doc, num, allowInternalOnly);
				}
				if (this.m_permSet == null)
				{
					this.m_permSet = new PermissionSet(PermissionState.None);
				}
			}
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x0009C7BC File Offset: 0x0009A9BC
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			PolicyStatement policyStatement = obj as PolicyStatement;
			return policyStatement != null && this.m_attributes == policyStatement.m_attributes && object.Equals(this.m_permSet, policyStatement.m_permSet);
		}

		// Token: 0x06002A10 RID: 10768 RVA: 0x0009C7FC File Offset: 0x0009A9FC
		[ComVisible(false)]
		public override int GetHashCode()
		{
			int num = (int)this.m_attributes;
			if (this.m_permSet != null)
			{
				num ^= this.m_permSet.GetHashCode();
			}
			return num;
		}

		// Token: 0x040010DA RID: 4314
		internal PermissionSet m_permSet;

		// Token: 0x040010DB RID: 4315
		[NonSerialized]
		private List<IDelayEvaluatedEvidence> m_dependentEvidence;

		// Token: 0x040010DC RID: 4316
		internal PolicyStatementAttribute m_attributes;
	}
}
