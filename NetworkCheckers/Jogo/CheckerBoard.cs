﻿using System;
using System.Globalization;

namespace NetworkCheckers.Jogo
{
    class CheckerBoard : IBoard
    {
        /// <summary>
        /// Peças no tabuleiro
        /// </summary>
        private readonly Piece[] _Pieces = new Piece[32];

        /// <summary>
        /// Get the size of the board.  This is the number of valid positions on the board.
        /// </summary>
        public int Size
        {
            get { return this._Pieces.Length; }
        }

        /// <summary>
        /// Get the number of rows on the board
        /// </summary>
        public int Rows
        {
            get { return BoardConstants.Rows; }
        }

        /// <summary>
        /// Get the number of columns on the board
        /// </summary>
        public int Cols
        {
            get { return BoardConstants.Cols; }
        }

        /// <summary>
        /// Get the piece at the specified board notation position
        /// </summary>
        /// <param name="position">The position.  This starts at one instead of zero.</param>
        /// <returns>The piece at the given board notation position</returns>
        public Piece this[int position]
        {
            get
            {
                if ((position <= 0) || (position > BoardConstants.SquareCount))
                {
                    throw new ArgumentOutOfRangeException("position", "Position must be between 1 and 32 inclusive");
                }

                int idx = position - 1;
                return this._Pieces[idx];
            }

            set
            {
                if ((position <= 0) || (position > BoardConstants.SquareCount))
                {
                    throw new ArgumentOutOfRangeException("position", "Position must be between 1 and 32 inclusive");
                }

                int idx = position - 1;
                this._Pieces[idx] = value;
            }
        }

        /// <summary>
        /// Get the piece at the given location
        /// </summary>
        /// <param name="row">The row</param>
        /// <param name="col">The column</param>
        /// <returns>The piece at the given location</returns>
        public Piece this[int row, int col]
        {
            get
            {
                if ((row < 0) || (row >= BoardConstants.Rows))
                {
                    throw new ArgumentOutOfRangeException("row", string.Format(CultureInfo.InvariantCulture, "must be between 0 and {0}", BoardConstants.Rows.ToString(CultureInfo.InvariantCulture)));
                }
                else if ((col < 0) || (col >= BoardConstants.Cols))
                {
                    throw new ArgumentOutOfRangeException("col", string.Format(CultureInfo.InvariantCulture, "must be between 0 and {0}", BoardConstants.Cols.ToString(CultureInfo.InvariantCulture)));
                }

                int position = Location.Notation(row, col);
                if ((position <= 0) || (position > BoardConstants.SquareCount))
                {
                    return Piece.Illegal;
                }

                return this[position];
            }

            set
            {
                int position = Location.Notation(row, col);
                this[position] = value;
            }
        }

        /// <summary>
        /// Clear the board
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < this._Pieces.Length; i++)
            {
                this._Pieces[i] = Piece.None;
            }
        }

        /// <summary>
        /// Generate a copy of the board
        /// </summary>
        /// <returns>A copy of this board</returns>
        public IBoard Copy()
        {
            CheckerBoard board = new CheckerBoard();
            Array.Copy(this._Pieces, board._Pieces, this._Pieces.Length);
            return board;
        }

        /// <summary>
        /// Copy the state of the given board
        /// </summary>
        /// <param name="board">The board to copy</param>
        public void Copy(IBoard board)
        {
            if (board.Size != this.Size)
            {
                throw new ArgumentException("Incompatable board sizes");
            }

            CheckerBoard checkerboard = board as CheckerBoard;
            if (checkerboard != null)
            {
                // Copy board in an optimized fashion
                this.Copy(checkerboard);
            }
            else
            {
                for (int i = 1; i <= _Pieces.Length; i++)
                {
                    this[i] = board[i];
                }
            }
        }

        /// <summary>
        /// Copy the state of the given checker board
        /// </summary>
        /// <param name="board">The checker board to copy</param>
        public void Copy(CheckerBoard board)
        {
            if (board == null)
            {
                throw new ArgumentNullException("board");
            }

            Array.Copy(board._Pieces, this._Pieces, this._Pieces.Length);
        }
    }
}
