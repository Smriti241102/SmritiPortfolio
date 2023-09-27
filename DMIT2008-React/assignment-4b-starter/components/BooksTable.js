import Paper from '@mui/material/Paper';

import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Button from '@mui/material/Button';

//importing router
import { useRouter } from 'next/router';

export default function BooksTable(props) {
  

  const router = useRouter();

  const handleBookClick = (book) => {
    // Extract the work ID from the book key
    
    const workId = book.split("/").pop();
    console.log(workId)
    router.push(`/book/${workId}`);
  };


    return <TableContainer component={Paper}>
    <Table>
      <TableHead>
        <TableRow>
          <TableCell>Books in all Languages</TableCell>
        </TableRow>
      </TableHead>
      <TableBody>
        {props.books.map((book,index)=> {
            return <TableRow
              key={index}
            >
               <TableCell>
                  {book.title}
              </TableCell>
              <TableCell>
              <Button onClick={() => handleBookClick(book.key)}>Details</Button>
            </TableCell>
            </TableRow>
          })}
      </TableBody>
    </Table>
  </TableContainer>
}