using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000771 RID: 1905
	internal sealed class ObjectReader
	{
		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x06005343 RID: 21315 RVA: 0x00124C98 File Offset: 0x00122E98
		private SerStack ValueFixupStack
		{
			get
			{
				if (this.valueFixupStack == null)
				{
					this.valueFixupStack = new SerStack("ValueType Fixup Stack");
				}
				return this.valueFixupStack;
			}
		}

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x06005344 RID: 21316 RVA: 0x00124CB8 File Offset: 0x00122EB8
		// (set) Token: 0x06005345 RID: 21317 RVA: 0x00124CC0 File Offset: 0x00122EC0
		internal object TopObject
		{
			get
			{
				return this.m_topObject;
			}
			set
			{
				this.m_topObject = value;
				if (this.m_objectManager != null)
				{
					this.m_objectManager.TopObject = value;
				}
			}
		}

		// Token: 0x06005346 RID: 21318 RVA: 0x00124CDD File Offset: 0x00122EDD
		internal void SetMethodCall(BinaryMethodCall binaryMethodCall)
		{
			this.bMethodCall = true;
			this.binaryMethodCall = binaryMethodCall;
		}

		// Token: 0x06005347 RID: 21319 RVA: 0x00124CED File Offset: 0x00122EED
		internal void SetMethodReturn(BinaryMethodReturn binaryMethodReturn)
		{
			this.bMethodReturn = true;
			this.binaryMethodReturn = binaryMethodReturn;
		}

		// Token: 0x06005348 RID: 21320 RVA: 0x00124D00 File Offset: 0x00122F00
		internal ObjectReader(Stream stream, ISurrogateSelector selector, StreamingContext context, InternalFE formatterEnums, SerializationBinder binder)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream", Environment.GetResourceString("ArgumentNull_Stream"));
			}
			this.m_stream = stream;
			this.m_surrogates = selector;
			this.m_context = context;
			this.m_binder = binder;
			if (this.m_binder != null)
			{
				ResourceReader.TypeLimitingDeserializationBinder typeLimitingDeserializationBinder = this.m_binder as ResourceReader.TypeLimitingDeserializationBinder;
				if (typeLimitingDeserializationBinder != null)
				{
					typeLimitingDeserializationBinder.ObjectReader = this;
				}
			}
			this.formatterEnums = formatterEnums;
		}

		// Token: 0x06005349 RID: 21321 RVA: 0x00124D7C File Offset: 0x00122F7C
		[SecurityCritical]
		internal object Deserialize(HeaderHandler handler, __BinaryParser serParser, bool fCheck, bool isCrossAppDomain, IMethodCallMessage methodCallMessage)
		{
			if (serParser == null)
			{
				throw new ArgumentNullException("serParser", Environment.GetResourceString("ArgumentNull_WithParamName", new object[]
				{
					serParser
				}));
			}
			this.bFullDeserialization = false;
			this.TopObject = null;
			this.topId = 0L;
			this.bMethodCall = false;
			this.bMethodReturn = false;
			this.bIsCrossAppDomain = isCrossAppDomain;
			this.bSimpleAssembly = (this.formatterEnums.FEassemblyFormat == FormatterAssemblyStyle.Simple);
			if (fCheck)
			{
				CodeAccessPermission.Demand(PermissionType.SecuritySerialization);
			}
			this.handler = handler;
			serParser.Run();
			if (this.bFullDeserialization)
			{
				this.m_objectManager.DoFixups();
			}
			if (!this.bMethodCall && !this.bMethodReturn)
			{
				if (this.TopObject == null)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_TopObject"));
				}
				if (this.HasSurrogate(this.TopObject.GetType()) && this.topId != 0L)
				{
					this.TopObject = this.m_objectManager.GetObject(this.topId);
				}
				if (this.TopObject is IObjectReference)
				{
					this.TopObject = ((IObjectReference)this.TopObject).GetRealObject(this.m_context);
				}
			}
			if (this.bFullDeserialization)
			{
				this.m_objectManager.RaiseDeserializationEvent();
			}
			if (handler != null)
			{
				this.handlerObject = handler(this.headers);
			}
			if (this.bMethodCall)
			{
				object[] callA = this.TopObject as object[];
				this.TopObject = this.binaryMethodCall.ReadArray(callA, this.handlerObject);
			}
			else if (this.bMethodReturn)
			{
				object[] returnA = this.TopObject as object[];
				this.TopObject = this.binaryMethodReturn.ReadArray(returnA, methodCallMessage, this.handlerObject);
			}
			return this.TopObject;
		}

		// Token: 0x0600534A RID: 21322 RVA: 0x00124F24 File Offset: 0x00123124
		[SecurityCritical]
		private bool HasSurrogate(Type t)
		{
			ISurrogateSelector surrogateSelector;
			return this.m_surrogates != null && this.m_surrogates.GetSurrogate(t, this.m_context, out surrogateSelector) != null;
		}

		// Token: 0x0600534B RID: 21323 RVA: 0x00124F52 File Offset: 0x00123152
		[SecurityCritical]
		private void CheckSerializable(Type t)
		{
			if (!t.IsSerializable && !this.HasSurrogate(t))
			{
				throw new SerializationException(string.Format(CultureInfo.InvariantCulture, Environment.GetResourceString("Serialization_NonSerType"), t.FullName, t.Assembly.FullName));
			}
		}

		// Token: 0x0600534C RID: 21324 RVA: 0x00124F90 File Offset: 0x00123190
		[SecurityCritical]
		private void InitFullDeserialization()
		{
			this.bFullDeserialization = true;
			this.stack = new SerStack("ObjectReader Object Stack");
			this.m_objectManager = new ObjectManager(this.m_surrogates, this.m_context, false, this.bIsCrossAppDomain);
			if (this.m_formatterConverter == null)
			{
				this.m_formatterConverter = new FormatterConverter();
			}
		}

		// Token: 0x0600534D RID: 21325 RVA: 0x00124FE5 File Offset: 0x001231E5
		internal object CrossAppDomainArray(int index)
		{
			return this.crossAppDomainArray[index];
		}

		// Token: 0x0600534E RID: 21326 RVA: 0x00124FEF File Offset: 0x001231EF
		[SecurityCritical]
		internal ReadObjectInfo CreateReadObjectInfo(Type objectType)
		{
			return ReadObjectInfo.Create(objectType, this.m_surrogates, this.m_context, this.m_objectManager, this.serObjectInfoInit, this.m_formatterConverter, this.bSimpleAssembly);
		}

		// Token: 0x0600534F RID: 21327 RVA: 0x0012501C File Offset: 0x0012321C
		[SecurityCritical]
		internal ReadObjectInfo CreateReadObjectInfo(Type objectType, string[] memberNames, Type[] memberTypes)
		{
			return ReadObjectInfo.Create(objectType, memberNames, memberTypes, this.m_surrogates, this.m_context, this.m_objectManager, this.serObjectInfoInit, this.m_formatterConverter, this.bSimpleAssembly);
		}

		// Token: 0x06005350 RID: 21328 RVA: 0x00125058 File Offset: 0x00123258
		[SecurityCritical]
		internal void Parse(ParseRecord pr)
		{
			switch (pr.PRparseTypeEnum)
			{
			case InternalParseTypeE.SerializedStreamHeader:
				this.ParseSerializedStreamHeader(pr);
				return;
			case InternalParseTypeE.Object:
				this.ParseObject(pr);
				return;
			case InternalParseTypeE.Member:
				this.ParseMember(pr);
				return;
			case InternalParseTypeE.ObjectEnd:
				this.ParseObjectEnd(pr);
				return;
			case InternalParseTypeE.MemberEnd:
				this.ParseMemberEnd(pr);
				return;
			case InternalParseTypeE.SerializedStreamHeaderEnd:
				this.ParseSerializedStreamHeaderEnd(pr);
				return;
			case InternalParseTypeE.Envelope:
			case InternalParseTypeE.EnvelopeEnd:
			case InternalParseTypeE.Body:
			case InternalParseTypeE.BodyEnd:
				return;
			}
			throw new SerializationException(Environment.GetResourceString("Serialization_XMLElement", new object[]
			{
				pr.PRname
			}));
		}

		// Token: 0x06005351 RID: 21329 RVA: 0x001250F8 File Offset: 0x001232F8
		private void ParseError(ParseRecord processing, ParseRecord onStack)
		{
			throw new SerializationException(Environment.GetResourceString("Serialization_ParseError", new object[]
			{
				string.Concat(new object[]
				{
					onStack.PRname,
					" ",
					onStack.PRparseTypeEnum,
					" ",
					processing.PRname,
					" ",
					processing.PRparseTypeEnum
				})
			}));
		}

		// Token: 0x06005352 RID: 21330 RVA: 0x0012516E File Offset: 0x0012336E
		private void ParseSerializedStreamHeader(ParseRecord pr)
		{
			this.stack.Push(pr);
		}

		// Token: 0x06005353 RID: 21331 RVA: 0x0012517C File Offset: 0x0012337C
		private void ParseSerializedStreamHeaderEnd(ParseRecord pr)
		{
			this.stack.Pop();
		}

		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x06005354 RID: 21332 RVA: 0x0012518A File Offset: 0x0012338A
		private bool IsRemoting
		{
			get
			{
				return this.bMethodCall || this.bMethodReturn;
			}
		}

		// Token: 0x06005355 RID: 21333 RVA: 0x0012519C File Offset: 0x0012339C
		[SecurityCritical]
		internal void CheckSecurity(ParseRecord pr)
		{
			Type prdtType = pr.PRdtType;
			if (prdtType != null && this.IsRemoting)
			{
				if (typeof(MarshalByRefObject).IsAssignableFrom(prdtType))
				{
					throw new ArgumentException(Environment.GetResourceString("Serialization_MBRAsMBV", new object[]
					{
						prdtType.FullName
					}));
				}
				FormatterServices.CheckTypeSecurity(prdtType, this.formatterEnums.FEsecurityLevel);
			}
		}

		// Token: 0x06005356 RID: 21334 RVA: 0x00125200 File Offset: 0x00123400
		[SecurityCritical]
		private void ParseObject(ParseRecord pr)
		{
			if (!this.bFullDeserialization)
			{
				this.InitFullDeserialization();
			}
			if (pr.PRobjectPositionEnum == InternalObjectPositionE.Top)
			{
				this.topId = pr.PRobjectId;
			}
			if (pr.PRparseTypeEnum == InternalParseTypeE.Object)
			{
				this.stack.Push(pr);
			}
			if (pr.PRobjectTypeEnum == InternalObjectTypeE.Array)
			{
				this.ParseArray(pr);
				return;
			}
			if (pr.PRdtType == null)
			{
				pr.PRnewObj = new TypeLoadExceptionHolder(pr.PRkeyDt);
				return;
			}
			if (pr.PRdtType == Converter.typeofString)
			{
				if (pr.PRvalue == null)
				{
					return;
				}
				pr.PRnewObj = pr.PRvalue;
				if (pr.PRobjectPositionEnum == InternalObjectPositionE.Top)
				{
					this.TopObject = pr.PRnewObj;
					return;
				}
				this.stack.Pop();
				this.RegisterObject(pr.PRnewObj, pr, (ParseRecord)this.stack.Peek());
				return;
			}
			else
			{
				this.CheckSerializable(pr.PRdtType);
				if (this.IsRemoting && this.formatterEnums.FEsecurityLevel != TypeFilterLevel.Full)
				{
					pr.PRnewObj = FormatterServices.GetSafeUninitializedObject(pr.PRdtType);
				}
				else
				{
					pr.PRnewObj = FormatterServices.GetUninitializedObject(pr.PRdtType);
				}
				this.m_objectManager.RaiseOnDeserializingEvent(pr.PRnewObj);
				if (pr.PRnewObj == null)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_TopObjectInstantiate", new object[]
					{
						pr.PRdtType
					}));
				}
				if (pr.PRobjectPositionEnum == InternalObjectPositionE.Top)
				{
					this.TopObject = pr.PRnewObj;
				}
				if (pr.PRobjectInfo == null)
				{
					pr.PRobjectInfo = ReadObjectInfo.Create(pr.PRdtType, this.m_surrogates, this.m_context, this.m_objectManager, this.serObjectInfoInit, this.m_formatterConverter, this.bSimpleAssembly);
				}
				this.CheckSecurity(pr);
				return;
			}
		}

		// Token: 0x06005357 RID: 21335 RVA: 0x001253AC File Offset: 0x001235AC
		[SecurityCritical]
		private void ParseObjectEnd(ParseRecord pr)
		{
			ParseRecord parseRecord = (ParseRecord)this.stack.Peek();
			if (parseRecord == null)
			{
				parseRecord = pr;
			}
			if (parseRecord.PRobjectPositionEnum == InternalObjectPositionE.Top && parseRecord.PRdtType == Converter.typeofString)
			{
				parseRecord.PRnewObj = parseRecord.PRvalue;
				this.TopObject = parseRecord.PRnewObj;
				return;
			}
			this.stack.Pop();
			ParseRecord parseRecord2 = (ParseRecord)this.stack.Peek();
			if (parseRecord.PRnewObj == null)
			{
				return;
			}
			if (parseRecord.PRobjectTypeEnum == InternalObjectTypeE.Array)
			{
				if (parseRecord.PRobjectPositionEnum == InternalObjectPositionE.Top)
				{
					this.TopObject = parseRecord.PRnewObj;
				}
				this.RegisterObject(parseRecord.PRnewObj, parseRecord, parseRecord2);
				return;
			}
			parseRecord.PRobjectInfo.PopulateObjectMembers(parseRecord.PRnewObj, parseRecord.PRmemberData);
			if (!parseRecord.PRisRegistered && parseRecord.PRobjectId > 0L)
			{
				this.RegisterObject(parseRecord.PRnewObj, parseRecord, parseRecord2);
			}
			if (parseRecord.PRisValueTypeFixup)
			{
				ValueFixup valueFixup = (ValueFixup)this.ValueFixupStack.Pop();
				valueFixup.Fixup(parseRecord, parseRecord2);
			}
			if (parseRecord.PRobjectPositionEnum == InternalObjectPositionE.Top)
			{
				this.TopObject = parseRecord.PRnewObj;
			}
			parseRecord.PRobjectInfo.ObjectEnd();
		}

		// Token: 0x06005358 RID: 21336 RVA: 0x001254CC File Offset: 0x001236CC
		[SecurityCritical]
		private void ParseArray(ParseRecord pr)
		{
			long probjectId = pr.PRobjectId;
			if (pr.PRarrayTypeEnum == InternalArrayTypeE.Base64)
			{
				if (pr.PRvalue.Length > 0)
				{
					pr.PRnewObj = Convert.FromBase64String(pr.PRvalue);
				}
				else
				{
					pr.PRnewObj = new byte[0];
				}
				if (this.stack.Peek() == pr)
				{
					this.stack.Pop();
				}
				if (pr.PRobjectPositionEnum == InternalObjectPositionE.Top)
				{
					this.TopObject = pr.PRnewObj;
				}
				ParseRecord objectPr = (ParseRecord)this.stack.Peek();
				this.RegisterObject(pr.PRnewObj, pr, objectPr);
			}
			else if (pr.PRnewObj != null && Converter.IsWriteAsByteArray(pr.PRarrayElementTypeCode))
			{
				if (pr.PRobjectPositionEnum == InternalObjectPositionE.Top)
				{
					this.TopObject = pr.PRnewObj;
				}
				ParseRecord objectPr2 = (ParseRecord)this.stack.Peek();
				this.RegisterObject(pr.PRnewObj, pr, objectPr2);
			}
			else if (pr.PRarrayTypeEnum == InternalArrayTypeE.Jagged || pr.PRarrayTypeEnum == InternalArrayTypeE.Single)
			{
				bool flag = true;
				if (pr.PRlowerBoundA == null || pr.PRlowerBoundA[0] == 0)
				{
					if (pr.PRarrayElementType == Converter.typeofString)
					{
						pr.PRobjectA = new string[pr.PRlengthA[0]];
						pr.PRnewObj = pr.PRobjectA;
						flag = false;
					}
					else if (pr.PRarrayElementType == Converter.typeofObject)
					{
						pr.PRobjectA = new object[pr.PRlengthA[0]];
						pr.PRnewObj = pr.PRobjectA;
						flag = false;
					}
					else if (pr.PRarrayElementType != null)
					{
						pr.PRnewObj = Array.UnsafeCreateInstance(pr.PRarrayElementType, pr.PRlengthA[0]);
					}
					pr.PRisLowerBound = false;
				}
				else
				{
					if (pr.PRarrayElementType != null)
					{
						pr.PRnewObj = Array.UnsafeCreateInstance(pr.PRarrayElementType, pr.PRlengthA, pr.PRlowerBoundA);
					}
					pr.PRisLowerBound = true;
				}
				if (pr.PRarrayTypeEnum == InternalArrayTypeE.Single)
				{
					if (!pr.PRisLowerBound && Converter.IsWriteAsByteArray(pr.PRarrayElementTypeCode))
					{
						pr.PRprimitiveArray = new PrimitiveArray(pr.PRarrayElementTypeCode, (Array)pr.PRnewObj);
					}
					else if (flag && pr.PRarrayElementType != null && !pr.PRarrayElementType.IsValueType && !pr.PRisLowerBound)
					{
						pr.PRobjectA = (object[])pr.PRnewObj;
					}
				}
				if (pr.PRobjectPositionEnum == InternalObjectPositionE.Headers)
				{
					this.headers = (Header[])pr.PRnewObj;
				}
				pr.PRindexMap = new int[1];
			}
			else
			{
				if (pr.PRarrayTypeEnum != InternalArrayTypeE.Rectangular)
				{
					throw new SerializationException(Environment.GetResourceString("Serialization_ArrayType", new object[]
					{
						pr.PRarrayTypeEnum
					}));
				}
				pr.PRisLowerBound = false;
				if (pr.PRlowerBoundA != null)
				{
					for (int i = 0; i < pr.PRrank; i++)
					{
						if (pr.PRlowerBoundA[i] != 0)
						{
							pr.PRisLowerBound = true;
						}
					}
				}
				if (pr.PRarrayElementType != null)
				{
					if (!pr.PRisLowerBound)
					{
						pr.PRnewObj = Array.UnsafeCreateInstance(pr.PRarrayElementType, pr.PRlengthA);
					}
					else
					{
						pr.PRnewObj = Array.UnsafeCreateInstance(pr.PRarrayElementType, pr.PRlengthA, pr.PRlowerBoundA);
					}
				}
				int num = 1;
				for (int j = 0; j < pr.PRrank; j++)
				{
					num *= pr.PRlengthA[j];
				}
				pr.PRindexMap = new int[pr.PRrank];
				pr.PRrectangularMap = new int[pr.PRrank];
				pr.PRlinearlength = num;
			}
			this.CheckSecurity(pr);
		}

		// Token: 0x06005359 RID: 21337 RVA: 0x0012583C File Offset: 0x00123A3C
		private void NextRectangleMap(ParseRecord pr)
		{
			for (int i = pr.PRrank - 1; i > -1; i--)
			{
				if (pr.PRrectangularMap[i] < pr.PRlengthA[i] - 1)
				{
					pr.PRrectangularMap[i]++;
					if (i < pr.PRrank - 1)
					{
						for (int j = i + 1; j < pr.PRrank; j++)
						{
							pr.PRrectangularMap[j] = 0;
						}
					}
					Array.Copy(pr.PRrectangularMap, pr.PRindexMap, pr.PRrank);
					return;
				}
			}
		}

		// Token: 0x0600535A RID: 21338 RVA: 0x001258C0 File Offset: 0x00123AC0
		[SecurityCritical]
		private void ParseArrayMember(ParseRecord pr)
		{
			ParseRecord parseRecord = (ParseRecord)this.stack.Peek();
			if (parseRecord.PRarrayTypeEnum == InternalArrayTypeE.Rectangular)
			{
				if (parseRecord.PRmemberIndex > 0)
				{
					this.NextRectangleMap(parseRecord);
				}
				if (parseRecord.PRisLowerBound)
				{
					for (int i = 0; i < parseRecord.PRrank; i++)
					{
						parseRecord.PRindexMap[i] = parseRecord.PRrectangularMap[i] + parseRecord.PRlowerBoundA[i];
					}
				}
			}
			else if (!parseRecord.PRisLowerBound)
			{
				parseRecord.PRindexMap[0] = parseRecord.PRmemberIndex;
			}
			else
			{
				parseRecord.PRindexMap[0] = parseRecord.PRlowerBoundA[0] + parseRecord.PRmemberIndex;
			}
			if (pr.PRmemberValueEnum == InternalMemberValueE.Reference)
			{
				object @object = this.m_objectManager.GetObject(pr.PRidRef);
				if (@object == null)
				{
					int[] array = new int[parseRecord.PRrank];
					Array.Copy(parseRecord.PRindexMap, 0, array, 0, parseRecord.PRrank);
					this.m_objectManager.RecordArrayElementFixup(parseRecord.PRobjectId, array, pr.PRidRef);
				}
				else if (parseRecord.PRobjectA != null)
				{
					parseRecord.PRobjectA[parseRecord.PRindexMap[0]] = @object;
				}
				else
				{
					((Array)parseRecord.PRnewObj).SetValue(@object, parseRecord.PRindexMap);
				}
			}
			else if (pr.PRmemberValueEnum == InternalMemberValueE.Nested)
			{
				if (pr.PRdtType == null)
				{
					pr.PRdtType = parseRecord.PRarrayElementType;
				}
				this.ParseObject(pr);
				this.stack.Push(pr);
				if (parseRecord.PRarrayElementType != null)
				{
					if (parseRecord.PRarrayElementType.IsValueType && pr.PRarrayElementTypeCode == InternalPrimitiveTypeE.Invalid)
					{
						pr.PRisValueTypeFixup = true;
						this.ValueFixupStack.Push(new ValueFixup((Array)parseRecord.PRnewObj, parseRecord.PRindexMap));
					}
					else if (parseRecord.PRobjectA != null)
					{
						parseRecord.PRobjectA[parseRecord.PRindexMap[0]] = pr.PRnewObj;
					}
					else
					{
						((Array)parseRecord.PRnewObj).SetValue(pr.PRnewObj, parseRecord.PRindexMap);
					}
				}
			}
			else if (pr.PRmemberValueEnum == InternalMemberValueE.InlineValue)
			{
				if (parseRecord.PRarrayElementType == Converter.typeofString || pr.PRdtType == Converter.typeofString)
				{
					this.ParseString(pr, parseRecord);
					if (parseRecord.PRobjectA != null)
					{
						parseRecord.PRobjectA[parseRecord.PRindexMap[0]] = pr.PRvalue;
					}
					else
					{
						((Array)parseRecord.PRnewObj).SetValue(pr.PRvalue, parseRecord.PRindexMap);
					}
				}
				else if (parseRecord.PRisArrayVariant)
				{
					if (pr.PRkeyDt == null)
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_ArrayTypeObject"));
					}
					object obj;
					if (pr.PRdtType == Converter.typeofString)
					{
						this.ParseString(pr, parseRecord);
						obj = pr.PRvalue;
					}
					else if (pr.PRdtTypeCode == InternalPrimitiveTypeE.Invalid)
					{
						this.CheckSerializable(pr.PRdtType);
						if (this.IsRemoting && this.formatterEnums.FEsecurityLevel != TypeFilterLevel.Full)
						{
							obj = FormatterServices.GetSafeUninitializedObject(pr.PRdtType);
						}
						else
						{
							obj = FormatterServices.GetUninitializedObject(pr.PRdtType);
						}
					}
					else if (pr.PRvarValue != null)
					{
						obj = pr.PRvarValue;
					}
					else
					{
						obj = Converter.FromString(pr.PRvalue, pr.PRdtTypeCode);
					}
					if (parseRecord.PRobjectA != null)
					{
						parseRecord.PRobjectA[parseRecord.PRindexMap[0]] = obj;
					}
					else
					{
						((Array)parseRecord.PRnewObj).SetValue(obj, parseRecord.PRindexMap);
					}
				}
				else if (parseRecord.PRprimitiveArray != null)
				{
					parseRecord.PRprimitiveArray.SetValue(pr.PRvalue, parseRecord.PRindexMap[0]);
				}
				else
				{
					object obj2;
					if (pr.PRvarValue != null)
					{
						obj2 = pr.PRvarValue;
					}
					else
					{
						obj2 = Converter.FromString(pr.PRvalue, parseRecord.PRarrayElementTypeCode);
					}
					if (parseRecord.PRobjectA != null)
					{
						parseRecord.PRobjectA[parseRecord.PRindexMap[0]] = obj2;
					}
					else
					{
						((Array)parseRecord.PRnewObj).SetValue(obj2, parseRecord.PRindexMap);
					}
				}
			}
			else if (pr.PRmemberValueEnum == InternalMemberValueE.Null)
			{
				parseRecord.PRmemberIndex += pr.PRnullCount - 1;
			}
			else
			{
				this.ParseError(pr, parseRecord);
			}
			parseRecord.PRmemberIndex++;
		}

		// Token: 0x0600535B RID: 21339 RVA: 0x00125CD6 File Offset: 0x00123ED6
		[SecurityCritical]
		private void ParseArrayMemberEnd(ParseRecord pr)
		{
			if (pr.PRmemberValueEnum == InternalMemberValueE.Nested)
			{
				this.ParseObjectEnd(pr);
			}
		}

		// Token: 0x0600535C RID: 21340 RVA: 0x00125CE8 File Offset: 0x00123EE8
		[SecurityCritical]
		private void ParseMember(ParseRecord pr)
		{
			ParseRecord parseRecord = (ParseRecord)this.stack.Peek();
			if (parseRecord != null)
			{
				string prname = parseRecord.PRname;
			}
			InternalMemberTypeE prmemberTypeEnum = pr.PRmemberTypeEnum;
			if (prmemberTypeEnum != InternalMemberTypeE.Field && prmemberTypeEnum == InternalMemberTypeE.Item)
			{
				this.ParseArrayMember(pr);
				return;
			}
			if (pr.PRdtType == null && parseRecord.PRobjectInfo.isTyped)
			{
				pr.PRdtType = parseRecord.PRobjectInfo.GetType(pr.PRname);
				if (pr.PRdtType != null)
				{
					pr.PRdtTypeCode = Converter.ToCode(pr.PRdtType);
				}
			}
			if (pr.PRmemberValueEnum == InternalMemberValueE.Null)
			{
				parseRecord.PRobjectInfo.AddValue(pr.PRname, null, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
				return;
			}
			if (pr.PRmemberValueEnum == InternalMemberValueE.Nested)
			{
				this.ParseObject(pr);
				this.stack.Push(pr);
				if (pr.PRobjectInfo != null && pr.PRobjectInfo.objectType != null && pr.PRobjectInfo.objectType.IsValueType)
				{
					pr.PRisValueTypeFixup = true;
					this.ValueFixupStack.Push(new ValueFixup(parseRecord.PRnewObj, pr.PRname, parseRecord.PRobjectInfo));
					return;
				}
				parseRecord.PRobjectInfo.AddValue(pr.PRname, pr.PRnewObj, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
				return;
			}
			else
			{
				if (pr.PRmemberValueEnum != InternalMemberValueE.Reference)
				{
					if (pr.PRmemberValueEnum == InternalMemberValueE.InlineValue)
					{
						if (pr.PRdtType == Converter.typeofString)
						{
							this.ParseString(pr, parseRecord);
							parseRecord.PRobjectInfo.AddValue(pr.PRname, pr.PRvalue, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
							return;
						}
						if (pr.PRdtTypeCode != InternalPrimitiveTypeE.Invalid)
						{
							object value;
							if (pr.PRvarValue != null)
							{
								value = pr.PRvarValue;
							}
							else
							{
								value = Converter.FromString(pr.PRvalue, pr.PRdtTypeCode);
							}
							parseRecord.PRobjectInfo.AddValue(pr.PRname, value, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
							return;
						}
						if (pr.PRarrayTypeEnum == InternalArrayTypeE.Base64)
						{
							parseRecord.PRobjectInfo.AddValue(pr.PRname, Convert.FromBase64String(pr.PRvalue), ref parseRecord.PRsi, ref parseRecord.PRmemberData);
							return;
						}
						if (pr.PRdtType == Converter.typeofObject)
						{
							throw new SerializationException(Environment.GetResourceString("Serialization_TypeMissing", new object[]
							{
								pr.PRname
							}));
						}
						this.ParseString(pr, parseRecord);
						if (pr.PRdtType == Converter.typeofSystemVoid)
						{
							parseRecord.PRobjectInfo.AddValue(pr.PRname, pr.PRdtType, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
							return;
						}
						if (parseRecord.PRobjectInfo.isSi)
						{
							parseRecord.PRobjectInfo.AddValue(pr.PRname, pr.PRvalue, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
							return;
						}
					}
					else
					{
						this.ParseError(pr, parseRecord);
					}
					return;
				}
				object @object = this.m_objectManager.GetObject(pr.PRidRef);
				if (@object == null)
				{
					parseRecord.PRobjectInfo.AddValue(pr.PRname, null, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
					parseRecord.PRobjectInfo.RecordFixup(parseRecord.PRobjectId, pr.PRname, pr.PRidRef);
					return;
				}
				parseRecord.PRobjectInfo.AddValue(pr.PRname, @object, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
				return;
			}
		}

		// Token: 0x0600535D RID: 21341 RVA: 0x0012600C File Offset: 0x0012420C
		[SecurityCritical]
		private void ParseMemberEnd(ParseRecord pr)
		{
			InternalMemberTypeE prmemberTypeEnum = pr.PRmemberTypeEnum;
			if (prmemberTypeEnum != InternalMemberTypeE.Field)
			{
				if (prmemberTypeEnum == InternalMemberTypeE.Item)
				{
					this.ParseArrayMemberEnd(pr);
					return;
				}
				this.ParseError(pr, (ParseRecord)this.stack.Peek());
			}
			else if (pr.PRmemberValueEnum == InternalMemberValueE.Nested)
			{
				this.ParseObjectEnd(pr);
				return;
			}
		}

		// Token: 0x0600535E RID: 21342 RVA: 0x00126058 File Offset: 0x00124258
		[SecurityCritical]
		private void ParseString(ParseRecord pr, ParseRecord parentPr)
		{
			if (!pr.PRisRegistered && pr.PRobjectId > 0L)
			{
				this.RegisterObject(pr.PRvalue, pr, parentPr, true);
			}
		}

		// Token: 0x0600535F RID: 21343 RVA: 0x0012607B File Offset: 0x0012427B
		[SecurityCritical]
		private void RegisterObject(object obj, ParseRecord pr, ParseRecord objectPr)
		{
			this.RegisterObject(obj, pr, objectPr, false);
		}

		// Token: 0x06005360 RID: 21344 RVA: 0x00126088 File Offset: 0x00124288
		[SecurityCritical]
		private void RegisterObject(object obj, ParseRecord pr, ParseRecord objectPr, bool bIsString)
		{
			if (!pr.PRisRegistered)
			{
				pr.PRisRegistered = true;
				long idOfContainingObj = 0L;
				MemberInfo member = null;
				int[] arrayIndex = null;
				if (objectPr != null)
				{
					arrayIndex = objectPr.PRindexMap;
					idOfContainingObj = objectPr.PRobjectId;
					if (objectPr.PRobjectInfo != null && !objectPr.PRobjectInfo.isSi)
					{
						member = objectPr.PRobjectInfo.GetMemberInfo(pr.PRname);
					}
				}
				SerializationInfo prsi = pr.PRsi;
				if (bIsString)
				{
					this.m_objectManager.RegisterString((string)obj, pr.PRobjectId, prsi, idOfContainingObj, member);
					return;
				}
				this.m_objectManager.RegisterObject(obj, pr.PRobjectId, prsi, idOfContainingObj, member, arrayIndex);
			}
		}

		// Token: 0x06005361 RID: 21345 RVA: 0x00126124 File Offset: 0x00124324
		[SecurityCritical]
		internal long GetId(long objectId)
		{
			if (!this.bFullDeserialization)
			{
				this.InitFullDeserialization();
			}
			if (objectId > 0L)
			{
				return objectId;
			}
			if (this.bOldFormatDetected || objectId == -1L)
			{
				this.bOldFormatDetected = true;
				if (this.valTypeObjectIdTable == null)
				{
					this.valTypeObjectIdTable = new IntSizedArray();
				}
				long num;
				if ((num = (long)this.valTypeObjectIdTable[(int)objectId]) == 0L)
				{
					num = 2147483647L + objectId;
					this.valTypeObjectIdTable[(int)objectId] = (int)num;
				}
				return num;
			}
			return -1L * objectId;
		}

		// Token: 0x06005362 RID: 21346 RVA: 0x001261A0 File Offset: 0x001243A0
		[Conditional("SER_LOGGING")]
		private void IndexTraceMessage(string message, int[] index)
		{
			StringBuilder stringBuilder = StringBuilderCache.Acquire(10);
			stringBuilder.Append("[");
			for (int i = 0; i < index.Length; i++)
			{
				stringBuilder.Append(index[i]);
				if (i != index.Length - 1)
				{
					stringBuilder.Append(",");
				}
			}
			stringBuilder.Append("]");
		}

		// Token: 0x06005363 RID: 21347 RVA: 0x001261FC File Offset: 0x001243FC
		[SecurityCritical]
		internal Type Bind(string assemblyString, string typeString)
		{
			Type type = null;
			if (this.m_binder != null)
			{
				type = this.m_binder.BindToType(assemblyString, typeString);
			}
			if (type == null)
			{
				type = this.FastBindToType(assemblyString, typeString);
			}
			return type;
		}

		// Token: 0x06005364 RID: 21348 RVA: 0x00126230 File Offset: 0x00124430
		[SecurityCritical]
		internal Type FastBindToType(string assemblyName, string typeName)
		{
			Type type = null;
			ObjectReader.TypeNAssembly typeNAssembly = (ObjectReader.TypeNAssembly)this.typeCache.GetCachedValue(typeName);
			if (typeNAssembly == null || typeNAssembly.assemblyName != assemblyName)
			{
				Assembly assembly = null;
				if (this.bSimpleAssembly)
				{
					try
					{
						ObjectReader.sfileIOPermission.Assert();
						try
						{
							assembly = ObjectReader.ResolveSimpleAssemblyName(new AssemblyName(assemblyName));
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
					}
					catch (Exception ex)
					{
					}
					if (assembly == null)
					{
						return null;
					}
					ObjectReader.GetSimplyNamedTypeFromAssembly(assembly, typeName, ref type);
				}
				else
				{
					try
					{
						ObjectReader.sfileIOPermission.Assert();
						try
						{
							assembly = Assembly.Load(assemblyName);
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
					}
					catch (Exception ex2)
					{
					}
					if (assembly == null)
					{
						return null;
					}
					type = FormatterServices.GetTypeFromAssembly(assembly, typeName);
				}
				if (type == null)
				{
					return null;
				}
				ObjectReader.CheckTypeForwardedTo(assembly, type.Assembly, type);
				typeNAssembly = new ObjectReader.TypeNAssembly();
				typeNAssembly.type = type;
				typeNAssembly.assemblyName = assemblyName;
				this.typeCache.SetCachedValue(typeNAssembly);
			}
			return typeNAssembly.type;
		}

		// Token: 0x06005365 RID: 21349 RVA: 0x00126348 File Offset: 0x00124548
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static Assembly ResolveSimpleAssemblyName(AssemblyName assemblyName)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMe;
			Assembly assembly = RuntimeAssembly.LoadWithPartialNameInternal(assemblyName, null, ref stackCrawlMark);
			if (assembly == null && assemblyName != null)
			{
				assembly = RuntimeAssembly.LoadWithPartialNameInternal(assemblyName.Name, null, ref stackCrawlMark);
			}
			return assembly;
		}

		// Token: 0x06005366 RID: 21350 RVA: 0x00126380 File Offset: 0x00124580
		[SecurityCritical]
		private static void GetSimplyNamedTypeFromAssembly(Assembly assm, string typeName, ref Type type)
		{
			try
			{
				type = FormatterServices.GetTypeFromAssembly(assm, typeName);
			}
			catch (TypeLoadException)
			{
			}
			catch (FileNotFoundException)
			{
			}
			catch (FileLoadException)
			{
			}
			catch (BadImageFormatException)
			{
			}
			if (type == null)
			{
				type = Type.GetType(typeName, new Func<AssemblyName, Assembly>(ObjectReader.ResolveSimpleAssemblyName), new Func<Assembly, string, bool, Type>(new ObjectReader.TopLevelAssemblyTypeResolver(assm).ResolveType), false);
			}
		}

		// Token: 0x06005367 RID: 21351 RVA: 0x00126404 File Offset: 0x00124604
		[SecurityCritical]
		internal Type GetType(BinaryAssemblyInfo assemblyInfo, string name)
		{
			Type type = null;
			if (this.previousName != null && this.previousName.Length == name.Length && this.previousName.Equals(name) && this.previousAssemblyString != null && this.previousAssemblyString.Length == assemblyInfo.assemblyString.Length && this.previousAssemblyString.Equals(assemblyInfo.assemblyString))
			{
				type = this.previousType;
			}
			else
			{
				type = this.Bind(assemblyInfo.assemblyString, name);
				if (type == null)
				{
					Assembly assembly = assemblyInfo.GetAssembly();
					if (this.bSimpleAssembly)
					{
						ObjectReader.GetSimplyNamedTypeFromAssembly(assembly, name, ref type);
					}
					else
					{
						type = FormatterServices.GetTypeFromAssembly(assembly, name);
					}
					if (type != null)
					{
						ObjectReader.CheckTypeForwardedTo(assembly, type.Assembly, type);
					}
				}
				this.previousAssemblyString = assemblyInfo.assemblyString;
				this.previousName = name;
				this.previousType = type;
			}
			return type;
		}

		// Token: 0x06005368 RID: 21352 RVA: 0x001264DC File Offset: 0x001246DC
		[SecuritySafeCritical]
		private static void CheckTypeForwardedTo(Assembly sourceAssembly, Assembly destAssembly, Type resolvedType)
		{
			if (!FormatterServices.UnsafeTypeForwardersIsEnabled() && sourceAssembly != destAssembly && !destAssembly.PermissionSet.IsSubsetOf(sourceAssembly.PermissionSet))
			{
				TypeInformation typeInformation = BinaryFormatter.GetTypeInformation(resolvedType);
				if (!typeInformation.HasTypeForwardedFrom)
				{
					throw new SecurityException
					{
						Demanded = sourceAssembly.PermissionSet
					};
				}
				Assembly left = null;
				try
				{
					left = Assembly.Load(typeInformation.AssemblyString);
				}
				catch
				{
				}
				if (left != sourceAssembly)
				{
					throw new SecurityException
					{
						Demanded = sourceAssembly.PermissionSet
					};
				}
			}
		}

		// Token: 0x040025C2 RID: 9666
		internal Stream m_stream;

		// Token: 0x040025C3 RID: 9667
		internal ISurrogateSelector m_surrogates;

		// Token: 0x040025C4 RID: 9668
		internal StreamingContext m_context;

		// Token: 0x040025C5 RID: 9669
		internal ObjectManager m_objectManager;

		// Token: 0x040025C6 RID: 9670
		internal InternalFE formatterEnums;

		// Token: 0x040025C7 RID: 9671
		internal SerializationBinder m_binder;

		// Token: 0x040025C8 RID: 9672
		internal long topId;

		// Token: 0x040025C9 RID: 9673
		internal bool bSimpleAssembly;

		// Token: 0x040025CA RID: 9674
		internal object handlerObject;

		// Token: 0x040025CB RID: 9675
		internal object m_topObject;

		// Token: 0x040025CC RID: 9676
		internal Header[] headers;

		// Token: 0x040025CD RID: 9677
		internal HeaderHandler handler;

		// Token: 0x040025CE RID: 9678
		internal SerObjectInfoInit serObjectInfoInit;

		// Token: 0x040025CF RID: 9679
		internal IFormatterConverter m_formatterConverter;

		// Token: 0x040025D0 RID: 9680
		internal SerStack stack;

		// Token: 0x040025D1 RID: 9681
		private SerStack valueFixupStack;

		// Token: 0x040025D2 RID: 9682
		internal object[] crossAppDomainArray;

		// Token: 0x040025D3 RID: 9683
		private bool bFullDeserialization;

		// Token: 0x040025D4 RID: 9684
		private bool bMethodCall;

		// Token: 0x040025D5 RID: 9685
		private bool bMethodReturn;

		// Token: 0x040025D6 RID: 9686
		private BinaryMethodCall binaryMethodCall;

		// Token: 0x040025D7 RID: 9687
		private BinaryMethodReturn binaryMethodReturn;

		// Token: 0x040025D8 RID: 9688
		private bool bIsCrossAppDomain;

		// Token: 0x040025D9 RID: 9689
		private static FileIOPermission sfileIOPermission = new FileIOPermission(PermissionState.Unrestricted);

		// Token: 0x040025DA RID: 9690
		private const int THRESHOLD_FOR_VALUETYPE_IDS = 2147483647;

		// Token: 0x040025DB RID: 9691
		private bool bOldFormatDetected;

		// Token: 0x040025DC RID: 9692
		private IntSizedArray valTypeObjectIdTable;

		// Token: 0x040025DD RID: 9693
		private NameCache typeCache = new NameCache();

		// Token: 0x040025DE RID: 9694
		private string previousAssemblyString;

		// Token: 0x040025DF RID: 9695
		private string previousName;

		// Token: 0x040025E0 RID: 9696
		private Type previousType;

		// Token: 0x02000C33 RID: 3123
		internal class TypeNAssembly
		{
			// Token: 0x040036EB RID: 14059
			public Type type;

			// Token: 0x040036EC RID: 14060
			public string assemblyName;
		}

		// Token: 0x02000C34 RID: 3124
		internal sealed class TopLevelAssemblyTypeResolver
		{
			// Token: 0x06006F5C RID: 28508 RVA: 0x0017EA62 File Offset: 0x0017CC62
			public TopLevelAssemblyTypeResolver(Assembly topLevelAssembly)
			{
				this.m_topLevelAssembly = topLevelAssembly;
			}

			// Token: 0x06006F5D RID: 28509 RVA: 0x0017EA71 File Offset: 0x0017CC71
			public Type ResolveType(Assembly assembly, string simpleTypeName, bool ignoreCase)
			{
				if (assembly == null)
				{
					assembly = this.m_topLevelAssembly;
				}
				return assembly.GetType(simpleTypeName, false, ignoreCase);
			}

			// Token: 0x040036ED RID: 14061
			private Assembly m_topLevelAssembly;
		}
	}
}
