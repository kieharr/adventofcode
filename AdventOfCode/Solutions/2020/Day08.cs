namespace AdventOfCode.Solutions._2020;

public class Day08: ASolution
{
    public override object Part1()
    {
        var program = Input.Select(x => new Instruction(x)).ToList();
        var computer = new Computer();
        computer.RunProgram(program);
        return computer.Accumulator;
    }

    public override object Part2()
    {
        var program = Input.Select(x => new Instruction(x)).ToList();
        var computer = new Computer();

        foreach (var instruction in program.Where(x => x.Code is Instructions.Jump or Instructions.NoOperation))
        {
            FlipInstruction(instruction);
            var result = computer.RunProgram(program);
            if (result != null)
            {
                return result;
            }
            FlipInstruction(instruction);
        }
        return null;
    }
    
    private void FlipInstruction(Instruction instruction)
    {
        instruction.Code = instruction.Code == Instructions.Jump ? Instructions.NoOperation : Instructions.Jump;
    }
    
    enum Instructions
    {
        NoOperation,
        Accumulator,
        Jump
    }

    class Instruction
    {
        public Instructions Code { get; set; }
        public int Value { get; }

        public Instruction(string line)
        {
            Code = line.Substring(0, 3) switch
            {
                "acc" => Instructions.Accumulator,
                "nop" => Instructions.NoOperation,
                "jmp" => Instructions.Jump,
                _ => throw new Exception("Invalid code")
            };
            Value = int.Parse(line.Substring(4));
        }
    }

    class Computer
    {
        public int Accumulator { get; private set; }
        private int _programCounter;

        public int? RunProgram(List<Instruction> program)
        {
            Accumulator = 0;
            _programCounter = 0;
            var visited = new HashSet<int>();
            while (true)
            {
                if (visited.Contains(_programCounter))
                {
                    return null;
                }
                if (_programCounter >= program.Count)
                    return Accumulator;
                var instruction = program[_programCounter];
                visited.Add(_programCounter);
                HandleInstruction(instruction);
            }
        }

        private void HandleInstruction(Instruction instruction)
        {
            switch (instruction.Code)
            {
                case Instructions.NoOperation:
                    _programCounter++;
                    break;
                case Instructions.Accumulator:
                    Accumulator += instruction.Value;
                    _programCounter++;
                    break;
                case Instructions.Jump:
                    _programCounter += instruction.Value;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}