import { useEffect } from 'react'
import confetti from 'canvas-confetti'

interface CelebrationProps {
  fire: boolean
}

export default function Celebration({ fire }: CelebrationProps) {
  useEffect(() => {
    if (!fire) return

    confetti({
      particleCount: 120,
      spread: 80,
      origin: { y: 0.7 },
      colors: ['#93c5fd', '#c4b5fd', '#fcd34d', '#f9a8d4'],
    })
  }, [fire])

  return null
}
