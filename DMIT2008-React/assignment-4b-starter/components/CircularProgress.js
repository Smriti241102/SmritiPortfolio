import CircularProgress from '@mui/material/CircularProgress';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';

export default function CircularLoading({ text }) {
  return (
    <Box sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
      <CircularProgress />
      <Typography variant="body1" sx={{ mt: 2 }}>
        {text}
      </Typography>
    </Box>
  );
}
