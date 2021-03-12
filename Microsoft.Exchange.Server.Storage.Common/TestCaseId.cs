using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x020000A2 RID: 162
	public struct TestCaseId : IComparable<TestCaseId>, IEquatable<TestCaseId>
	{
		// Token: 0x060007C8 RID: 1992 RVA: 0x00015C4C File Offset: 0x00013E4C
		private TestCaseId(int value)
		{
			this.value = value;
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x00015C55 File Offset: 0x00013E55
		public static TestCaseId Null
		{
			get
			{
				return TestCaseId.nullId;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x00015C5C File Offset: 0x00013E5C
		public static TestCaseId ExpandedConversationViewTestCaseId
		{
			get
			{
				return TestCaseId.expandedConversationViewTestCaseId;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x00015C63 File Offset: 0x00013E63
		public int Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x00015C6B File Offset: 0x00013E6B
		public bool IsNull
		{
			get
			{
				return this.value == -1;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x00015C76 File Offset: 0x00013E76
		public bool IsNotNull
		{
			get
			{
				return this.value != -1;
			}
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00015C84 File Offset: 0x00013E84
		public static bool operator ==(TestCaseId left, TestCaseId right)
		{
			return left.Value == right.Value;
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00015C96 File Offset: 0x00013E96
		public static bool operator !=(TestCaseId left, TestCaseId right)
		{
			return left.Value != right.Value;
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00015CAB File Offset: 0x00013EAB
		public static explicit operator TestCaseId(int value)
		{
			return new TestCaseId(value);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00015CB3 File Offset: 0x00013EB3
		public static TestCaseId GetInProcessTestCaseId()
		{
			if (!DefaultSettings.Get.EnableTestCaseIdLookup)
			{
				return TestCaseId.Null;
			}
			return TestCaseId.InternalGetTestCaseId();
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00015CCC File Offset: 0x00013ECC
		public override int GetHashCode()
		{
			return this.value;
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00015CD4 File Offset: 0x00013ED4
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.value.ToString();
			}
			return "Null";
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00015CEF File Offset: 0x00013EEF
		public override bool Equals(object other)
		{
			return other is TestCaseId && this.Equals((TestCaseId)other);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00015D07 File Offset: 0x00013F07
		public bool Equals(TestCaseId other)
		{
			return this.Value == other.Value;
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00015D18 File Offset: 0x00013F18
		public int CompareTo(TestCaseId other)
		{
			return this.Value - other.Value;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00015D28 File Offset: 0x00013F28
		private static TestCaseId InternalGetTestCaseId()
		{
			int num;
			if (int.TryParse(Environment.GetEnvironmentVariable("PerseusActiveTestCaseTCMID", EnvironmentVariableTarget.Process), out num))
			{
				return new TestCaseId(num);
			}
			return TestCaseId.Null;
		}

		// Token: 0x040006EC RID: 1772
		private const int NullTestCaseId = -1;

		// Token: 0x040006ED RID: 1773
		private static readonly TestCaseId nullId = new TestCaseId(-1);

		// Token: 0x040006EE RID: 1774
		private static readonly TestCaseId expandedConversationViewTestCaseId = new TestCaseId(380796);

		// Token: 0x040006EF RID: 1775
		private int value;
	}
}
