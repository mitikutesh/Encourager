import { motion } from 'framer-motion'
import { Sparkles } from 'lucide-react'

interface VerseDisplayProps {
  text: string
  reference: string
}

export default function VerseDisplay({ text, reference }: VerseDisplayProps) {
  return (
    <motion.div
      key={reference}
      initial={{ opacity: 0, y: 16 }}
      animate={{ opacity: 1, y: 0 }}
      transition={{ duration: 0.6, ease: 'easeOut' }}
      className="bg-white/70 backdrop-blur-sm rounded-3xl shadow-lg shadow-blue-100/50 px-8 py-10 max-w-sm w-full text-center"
    >
      <Sparkles className="w-6 h-6 text-amber-400 mx-auto mb-6" />

      <p className="text-xl leading-relaxed text-slate-700 font-serif italic">
        &ldquo;{text}&rdquo;
      </p>

      <p className="mt-5 text-sm font-semibold text-blue-600 tracking-wide uppercase">
        &mdash; {reference}
      </p>
    </motion.div>
  )
}
