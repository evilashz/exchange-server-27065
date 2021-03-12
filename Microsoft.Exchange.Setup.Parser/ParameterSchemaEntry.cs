using System;

namespace Microsoft.Exchange.Setup.Parser
{
	// Token: 0x02000009 RID: 9
	internal class ParameterSchemaEntry
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00004398 File Offset: 0x00002598
		public ParameterSchemaEntry(string name) : this(name, ParameterType.MustHaveValue)
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000043A2 File Offset: 0x000025A2
		public ParameterSchemaEntry(string name, ParameterType parameterType) : this(name, parameterType, SetupOperations.AllSetupOperations)
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000043B1 File Offset: 0x000025B1
		public ParameterSchemaEntry(string name, ParameterType parameterType, SetupOperations validOperations) : this(name, parameterType, validOperations, SetupRoles.AllRoles)
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000043C1 File Offset: 0x000025C1
		public ParameterSchemaEntry(string name, ParameterType parameterType, SetupOperations validOperations, SetupRoles validRoles) : this(name, parameterType, validOperations, validRoles, new ParseMethod(ParameterSchemaEntry.DefaultParser))
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000043DA File Offset: 0x000025DA
		public ParameterSchemaEntry(string name, ParameterType parameterType, SetupOperations validOperations, SetupRoles validRoles, ParseMethod parseMethod)
		{
			this.name = name;
			this.parameterType = parameterType;
			this.validOperations = validOperations;
			this.validRoles = validRoles;
			this.parseMethod = parseMethod;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00004407 File Offset: 0x00002607
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000440F File Offset: 0x0000260F
		public ParameterType ParameterType
		{
			get
			{
				return this.parameterType;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00004417 File Offset: 0x00002617
		public SetupOperations ValidOperations
		{
			get
			{
				return this.validOperations;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000441F File Offset: 0x0000261F
		public SetupRoles ValidRoles
		{
			get
			{
				return this.validRoles;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00004427 File Offset: 0x00002627
		public ParseMethod ParseMethod
		{
			get
			{
				return this.parseMethod;
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000442F File Offset: 0x0000262F
		public static object DefaultParser(string s)
		{
			return s;
		}

		// Token: 0x04000061 RID: 97
		private readonly string name;

		// Token: 0x04000062 RID: 98
		private readonly ParameterType parameterType;

		// Token: 0x04000063 RID: 99
		private readonly SetupOperations validOperations;

		// Token: 0x04000064 RID: 100
		private readonly SetupRoles validRoles;

		// Token: 0x04000065 RID: 101
		private readonly ParseMethod parseMethod;
	}
}
