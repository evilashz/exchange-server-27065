using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security;

namespace System.Reflection.Emit
{
	// Token: 0x02000607 RID: 1543
	internal class DynamicScope
	{
		// Token: 0x06004901 RID: 18689 RVA: 0x00107A5F File Offset: 0x00105C5F
		internal DynamicScope()
		{
			this.m_tokens = new List<object>();
			this.m_tokens.Add(null);
		}

		// Token: 0x17000B79 RID: 2937
		internal object this[int token]
		{
			get
			{
				token &= 16777215;
				if (token < 0 || token > this.m_tokens.Count)
				{
					return null;
				}
				return this.m_tokens[token];
			}
		}

		// Token: 0x06004903 RID: 18691 RVA: 0x00107AA9 File Offset: 0x00105CA9
		internal int GetTokenFor(VarArgMethod varArgMethod)
		{
			this.m_tokens.Add(varArgMethod);
			return this.m_tokens.Count - 1 | 167772160;
		}

		// Token: 0x06004904 RID: 18692 RVA: 0x00107ACA File Offset: 0x00105CCA
		internal string GetString(int token)
		{
			return this[token] as string;
		}

		// Token: 0x06004905 RID: 18693 RVA: 0x00107AD8 File Offset: 0x00105CD8
		internal byte[] ResolveSignature(int token, int fromMethod)
		{
			if (fromMethod == 0)
			{
				return (byte[])this[token];
			}
			VarArgMethod varArgMethod = this[token] as VarArgMethod;
			if (varArgMethod == null)
			{
				return null;
			}
			return varArgMethod.m_signature.GetSignature(true);
		}

		// Token: 0x06004906 RID: 18694 RVA: 0x00107B14 File Offset: 0x00105D14
		[SecuritySafeCritical]
		public int GetTokenFor(RuntimeMethodHandle method)
		{
			IRuntimeMethodInfo methodInfo = method.GetMethodInfo();
			RuntimeMethodHandleInternal value = methodInfo.Value;
			if (methodInfo != null && !RuntimeMethodHandle.IsDynamicMethod(value))
			{
				RuntimeType declaringType = RuntimeMethodHandle.GetDeclaringType(value);
				if (declaringType != null && RuntimeTypeHandle.HasInstantiation(declaringType))
				{
					MethodBase methodBase = RuntimeType.GetMethodBase(methodInfo);
					Type genericTypeDefinition = methodBase.DeclaringType.GetGenericTypeDefinition();
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_MethodDeclaringTypeGenericLcg"), methodBase, genericTypeDefinition));
				}
			}
			this.m_tokens.Add(method);
			return this.m_tokens.Count - 1 | 100663296;
		}

		// Token: 0x06004907 RID: 18695 RVA: 0x00107BA8 File Offset: 0x00105DA8
		public int GetTokenFor(RuntimeMethodHandle method, RuntimeTypeHandle typeContext)
		{
			this.m_tokens.Add(new GenericMethodInfo(method, typeContext));
			return this.m_tokens.Count - 1 | 100663296;
		}

		// Token: 0x06004908 RID: 18696 RVA: 0x00107BCF File Offset: 0x00105DCF
		public int GetTokenFor(DynamicMethod method)
		{
			this.m_tokens.Add(method);
			return this.m_tokens.Count - 1 | 100663296;
		}

		// Token: 0x06004909 RID: 18697 RVA: 0x00107BF0 File Offset: 0x00105DF0
		public int GetTokenFor(RuntimeFieldHandle field)
		{
			this.m_tokens.Add(field);
			return this.m_tokens.Count - 1 | 67108864;
		}

		// Token: 0x0600490A RID: 18698 RVA: 0x00107C16 File Offset: 0x00105E16
		public int GetTokenFor(RuntimeFieldHandle field, RuntimeTypeHandle typeContext)
		{
			this.m_tokens.Add(new GenericFieldInfo(field, typeContext));
			return this.m_tokens.Count - 1 | 67108864;
		}

		// Token: 0x0600490B RID: 18699 RVA: 0x00107C3D File Offset: 0x00105E3D
		public int GetTokenFor(RuntimeTypeHandle type)
		{
			this.m_tokens.Add(type);
			return this.m_tokens.Count - 1 | 33554432;
		}

		// Token: 0x0600490C RID: 18700 RVA: 0x00107C63 File Offset: 0x00105E63
		public int GetTokenFor(string literal)
		{
			this.m_tokens.Add(literal);
			return this.m_tokens.Count - 1 | 1879048192;
		}

		// Token: 0x0600490D RID: 18701 RVA: 0x00107C84 File Offset: 0x00105E84
		public int GetTokenFor(byte[] signature)
		{
			this.m_tokens.Add(signature);
			return this.m_tokens.Count - 1 | 285212672;
		}

		// Token: 0x04001DDA RID: 7642
		internal List<object> m_tokens;
	}
}
