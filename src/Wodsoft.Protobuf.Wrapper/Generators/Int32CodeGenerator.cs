﻿using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Wodsoft.Protobuf.Generators
{
    /// <summary>
    /// Int32 code generator.
    /// </summary>
    public class Int32CodeGenerator : StructureCodeGenerator<int>
    {
        public override int CalculateSize(int value)
        {
            return CodedOutputStream.ComputeInt32Size(value);
        }

        public override FieldCodec<int> CreateFieldCodec(int fieldNumber)
        {
            return FieldCodec.ForInt32(WireFormat.MakeTag(fieldNumber, WireType));
        }

        public override void GenerateCalculateSizeCode(ILGenerator ilGenerator, LocalBuilder valueVariable)
        {
            ilGenerator.Emit(OpCodes.Ldloc, valueVariable);
            ilGenerator.Emit(OpCodes.Call, typeof(CodedOutputStream).GetMethod(nameof(CodedOutputStream.ComputeInt32Size), BindingFlags.Static | BindingFlags.Public));
            ilGenerator.Emit(OpCodes.Ldc_I4_1);
            ilGenerator.Emit(OpCodes.Add_Ovf);
        }

        public override void GenerateReadCode(ILGenerator ilGenerator)
        {
            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Call, typeof(ParseContext).GetMethod(nameof(ParseContext.ReadInt32)));
        }

        public override int ReadValue(ref ParseContext parser)
        {
            return parser.ReadInt32();
        }

        public override void WriteValue(ref WriteContext writer, int value)
        {
            writer.WriteInt32(value);
        }

        protected override void GenerateWriteValueCode(ILGenerator ilGenerator, LocalBuilder valueVariable)
        {
            //Write bool value
            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Ldloc, valueVariable);
            ilGenerator.Emit(OpCodes.Call, typeof(WriteContext).GetMethod(nameof(WriteContext.WriteInt32)));
        }

    }
}
