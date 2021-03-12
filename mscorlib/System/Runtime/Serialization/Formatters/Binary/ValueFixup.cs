using System;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200076D RID: 1901
	internal sealed class ValueFixup
	{
		// Token: 0x06005337 RID: 21303 RVA: 0x00124821 File Offset: 0x00122A21
		internal ValueFixup(Array arrayObj, int[] indexMap)
		{
			this.valueFixupEnum = ValueFixupEnum.Array;
			this.arrayObj = arrayObj;
			this.indexMap = indexMap;
		}

		// Token: 0x06005338 RID: 21304 RVA: 0x0012483E File Offset: 0x00122A3E
		internal ValueFixup(object memberObject, string memberName, ReadObjectInfo objectInfo)
		{
			this.valueFixupEnum = ValueFixupEnum.Member;
			this.memberObject = memberObject;
			this.memberName = memberName;
			this.objectInfo = objectInfo;
		}

		// Token: 0x06005339 RID: 21305 RVA: 0x00124864 File Offset: 0x00122A64
		[SecurityCritical]
		internal void Fixup(ParseRecord record, ParseRecord parent)
		{
			object prnewObj = record.PRnewObj;
			switch (this.valueFixupEnum)
			{
			case ValueFixupEnum.Array:
				this.arrayObj.SetValue(prnewObj, this.indexMap);
				return;
			case ValueFixupEnum.Header:
			{
				Type typeFromHandle = typeof(Header);
				if (ValueFixup.valueInfo == null)
				{
					MemberInfo[] member = typeFromHandle.GetMember("Value");
					if (member.Length != 1)
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_HeaderReflection", new object[]
						{
							member.Length
						}));
					}
					ValueFixup.valueInfo = member[0];
				}
				FormatterServices.SerializationSetValue(ValueFixup.valueInfo, this.header, prnewObj);
				return;
			}
			case ValueFixupEnum.Member:
			{
				if (this.objectInfo.isSi)
				{
					this.objectInfo.objectManager.RecordDelayedFixup(parent.PRobjectId, this.memberName, record.PRobjectId);
					return;
				}
				MemberInfo memberInfo = this.objectInfo.GetMemberInfo(this.memberName);
				if (memberInfo != null)
				{
					this.objectInfo.objectManager.RecordFixup(parent.PRobjectId, memberInfo, record.PRobjectId);
				}
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x0400259D RID: 9629
		internal ValueFixupEnum valueFixupEnum;

		// Token: 0x0400259E RID: 9630
		internal Array arrayObj;

		// Token: 0x0400259F RID: 9631
		internal int[] indexMap;

		// Token: 0x040025A0 RID: 9632
		internal object header;

		// Token: 0x040025A1 RID: 9633
		internal object memberObject;

		// Token: 0x040025A2 RID: 9634
		internal static volatile MemberInfo valueInfo;

		// Token: 0x040025A3 RID: 9635
		internal ReadObjectInfo objectInfo;

		// Token: 0x040025A4 RID: 9636
		internal string memberName;
	}
}
